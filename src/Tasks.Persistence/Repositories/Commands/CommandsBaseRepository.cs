﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Tasks.Domain.Abstractions;
using Tasks.Domain.Abstractions.Dtos;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Shared;
using Tasks.Persistence.Attributes;
using Tasks.Persistence.Errors;

namespace Tasks.Persistence.Repositories.Commands
{
    internal class CommandsBaseRepository<TDomainEntity> : ICommandsRepository<TDomainEntity>
        where TDomainEntity : Entity
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TDomainEntity> DbSet;

        public CommandsBaseRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TDomainEntity>();
        }

        public virtual async Task CreateAsync(TDomainEntity domainModel, CancellationToken ct)
        {
            DbSet.Add(domainModel);

            await Context.SaveChangesAsync(ct);
        }

        public async Task<UpdateResponse<TDomainEntity>> UpdateAsync(TDomainEntity domainModel, CancellationToken ct)
        {
            var toUpdate = await DbSet.FindAsync(domainModel.Id);

            var response = new UpdateResponse<TDomainEntity>
            {
                NewValue = domainModel,
                OldValue = toUpdate
            };

            if (toUpdate != null)
            {
                // Update the main entity's properties
                Context.Entry(toUpdate).CurrentValues.SetValues(domainModel);

                // Update navigation properties
                UpdateNavigationProperties(toUpdate, domainModel);

                if (Context.Entry(toUpdate).State == EntityState.Detached)
                {
                    DbSet.Attach(toUpdate);
                }

                Context.Entry(toUpdate).State = EntityState.Modified;
            }

            await Context.SaveChangesAsync(ct);

            return response;
        }

        private void UpdateNavigationProperties(TDomainEntity toUpdate, TDomainEntity domainModel)
        {
            var navigationProperties = Context.Entry(toUpdate).Navigations;

            foreach (var navigation in navigationProperties)
            {
                var domainModelNavigation = Context.Entry(domainModel).Navigation(navigation.Metadata.Name);
                if (domainModelNavigation.CurrentValue != null)
                {
                    // Get the navigation property from the current entity and the domain model
                    var nestedEntityToUpdate = navigation.CurrentValue;
                    var nestedEntityDomainModel = domainModelNavigation.CurrentValue;

                    if (nestedEntityToUpdate == null)
                    {
                        // If the nested entity in the database is null, create a new instance and set it
                        nestedEntityToUpdate = Activator.CreateInstance(nestedEntityDomainModel.GetType());
                        navigation.CurrentValue = nestedEntityToUpdate;
                    }

                    // Update the nested entity's properties
                    Context.Entry(nestedEntityToUpdate).CurrentValues.SetValues(nestedEntityDomainModel);

                    // Ensure nested entity is marked as modified
                    Context.Entry(nestedEntityToUpdate).State = EntityState.Modified;
                }
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct, bool ignoreDependencies = false)
        {
            var toDelete = await IncludeDependents().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);

            if (toDelete  is null)
            {
                return;
            }

            if (!ignoreDependencies && !IsDeletionAllowed(Context.Entry(toDelete)))
            {
                var propertyWithAttribute = typeof(TDomainEntity)
                    .GetProperties()
                    .Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(UserFriendlyIdAttribute), true).FirstOrDefault() as UserFriendlyIdAttribute })
                    .FirstOrDefault(x => x.Attribute != null);

                var userFriendlyId = propertyWithAttribute?.Property.GetValue(toDelete, null);
                throw new DomainValidationException(DbErrors.SingleDeleteDeniedMessage(typeof(TDomainEntity).Name, userFriendlyId?.ToString()));
            }

            // Mark the entity and its dependents as deleted
            MarkEntityAsDeleted(toDelete);

            await Context.SaveChangesAsync(ct);
        }

        private void MarkEntityAsDeleted(TDomainEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entity.IsDeleted = true;

            foreach (var navigationEntry in Context.Entry(entity).Navigations.Where(x => !IsOwnedType(x)))
            {
                if (navigationEntry is CollectionEntry { CurrentValue: { } } collectionEntry)
                {
                    foreach (var dependentEntity in collectionEntry.CurrentValue)
                    {
                        if (dependentEntity is Entity dependent)
                        {
                            dependent.IsDeleted = true;
                        }
                    }
                }
                else if (navigationEntry is ReferenceEntry { CurrentValue: Entity dependent })
                {
                    dependent.IsDeleted = true;
                }
            }
        }

        public async Task DeleteAsync(Guid[] ids, CancellationToken ct, bool ignoreDependencies = false)
        {
            var toDelete = IncludeDependents().Where(w => ids.Contains(w.Id));

            foreach (var entity in toDelete)
            {
                if (!ignoreDependencies && !IsDeletionAllowed(Context.Entry(entity)))
                {
                    var propertyWithAttribute = typeof(TDomainEntity)
                        .GetProperties()
                        .Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(UserFriendlyIdAttribute), true).FirstOrDefault() as UserFriendlyIdAttribute })
                        .FirstOrDefault(x => x.Attribute != null);

                    var userFriendlyId = propertyWithAttribute?.Property.GetValue(entity, null);

                    throw new DomainValidationException(DbErrors.MultipleDeleteDeniedMessageInSingular(typeof(TDomainEntity).Name, userFriendlyId?.ToString()));
                }

                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                entity.IsDeleted = true;
            }

            await Context.SaveChangesAsync(ct);
        }

        protected virtual IQueryable<TDomainEntity> IncludeDependents()
        {
            var query = DbSet.AsQueryable();

            var entityType = Context.Model.FindEntityType(typeof(TDomainEntity));
            if (entityType == null)
            {
                return query;
            }

            var ownedTypes = Context.Model.GetEntityTypes()
                .Where(t => t.IsOwned())
                .Select(t => t.ClrType)
                .ToHashSet();

            var navigations = entityType
                .GetDerivedTypesInclusive()
                .SelectMany(type => type.GetNavigations().Where(x =>
                    x.PropertyInfo != null && !x.IsOnDependent &&
                    !x.PropertyInfo.GetCustomAttributes(typeof(IgnoreOnDeleteAttribute), true).Any() &&
                    !ownedTypes.Contains(x.ClrType)))
                .Distinct();

            if (navigations != null)
            {
                query = navigations.Aggregate(query, (current, property) => current.Include(property.Name));
            }

            return query;
        }

        protected virtual bool IsDeletionAllowed(EntityEntry entry)
        {
            var list = new List<EntityEntry> { entry };
            return IsDeletionAllowed(list);
        }

        protected virtual bool IsDeletionAllowed(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                foreach (var navigationEntry in entry.Navigations.Where(x =>
                             x.Metadata.PropertyInfo != null &&
                             !((IReadOnlyNavigation)x.Metadata).IsOnDependent &&
                             !x.Metadata.PropertyInfo.GetCustomAttributes(typeof(IgnoreOnDeleteAttribute), true).Any() &&
                             !IsOwnedType(x)))
                {
                    if (navigationEntry is CollectionEntry { CurrentValue: { } } collectionEntry)
                    {
                        foreach (var dependentEntry in collectionEntry.CurrentValue)
                        {
                            if (!IsDependentDeleted(dependentEntry))
                                return false;
                        }
                    }
                    else if (navigationEntry is ReferenceEntry { CurrentValue: { } } referenceEntry)
                    {
                        if (!IsDependentDeleted(referenceEntry.CurrentValue))
                            return false;
                    }
                }
            }

            return true;
        }

        private bool IsOwnedType(NavigationEntry navigationEntry)
        {
            var entityType = navigationEntry.Metadata.ClrType;
            return Context.Model.FindEntityType(entityType)?.IsOwned() == true;
        }

        protected virtual bool IsDependentDeleted(object? dependant)
        {
            if (dependant == null)
            {
                return true;
            }

            if (dependant is Entity e)
            {
                return e.IsDeleted;
            }

            return false;
        }
    }
}
