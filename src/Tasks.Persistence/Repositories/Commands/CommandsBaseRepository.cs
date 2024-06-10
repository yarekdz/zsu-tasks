using Microsoft.EntityFrameworkCore;
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
                Context.Entry(toUpdate).CurrentValues.SetValues(domainModel);

                if (Context.Entry(toUpdate).State == EntityState.Detached)
                {
                    DbSet.Attach(toUpdate);
                }

                Context.Entry(toUpdate).State = EntityState.Modified;
            }

            await Context.SaveChangesAsync(ct);

            return response;
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

            if (Context.Entry(toDelete).State == EntityState.Detached)
            {
                DbSet.Attach(toDelete);
            }

            toDelete.IsDeleted = true;

            await Context.SaveChangesAsync(ct);
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

            var navigations = Context.Model.FindEntityType(typeof(TDomainEntity))
                ?.GetDerivedTypesInclusive()
                .SelectMany(type => type.GetNavigations().Where(x =>
                    x.PropertyInfo != null && !x.IsOnDependent &&
                    !x.PropertyInfo.GetCustomAttributes(typeof(IgnoreOnDeleteAttribute), true).Any()))
                .Distinct();

            if (navigations != null)
                query = navigations.Aggregate(query, (current, property) => current.Include(property.Name));

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
                             x.Metadata.PropertyInfo != null && !((IReadOnlyNavigation)x.Metadata).IsOnDependent && !x.Metadata.PropertyInfo
                                 .GetCustomAttributes(typeof(IgnoreOnDeleteAttribute), true).Any()))
                {
                    if (navigationEntry is CollectionEntry { CurrentValue: { } } collectionEntry)
                    {
                        foreach (var dependentEntry in collectionEntry.CurrentValue)
                        {
                            if (!IsDependentDeleted(dependentEntry))
                                return false;
                        }
                    }
                    else
                    {
                        if (navigationEntry.CurrentValue != null && !IsDependentDeleted(navigationEntry.CurrentValue))
                            return false;
                    }
                }
            }

            return true;
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
