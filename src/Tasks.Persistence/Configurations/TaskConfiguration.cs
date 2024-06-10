using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain;
using Tasks.Domain.Person;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Persistence.Configurations
{
    internal class TaskConfiguration : IEntityTypeConfiguration<TodoTask>
    {
        public void Configure(EntityTypeBuilder<TodoTask> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.TaskId).IsUnique();

            builder.Property(t => t.TaskId)
                .HasColumnName(nameof(TodoTask.TaskId))
                .HasConversion(taskId => taskId.Value,
                    value => new TaskId(value));

            builder.OwnsOne(t => t.MainInfo, mib =>
            {
                mib.Property(mi => mi.Title).IsRequired().HasMaxLength(100);

                mib.Property(mi => mi.Description).HasMaxLength(500);

                mib.Property(mi => mi.Category).IsRequired().HasMaxLength(50)
                    .HasConversion(c => c.ToString(),
                        c => (Category)Enum.Parse(typeof(Category), c));

                mib.Property(mi => mi.Priority).IsRequired()
                    .HasConversion(pr => pr.Value,
                        pr => Priority.Create(pr));

                mib.Property(mi => mi.OwnerId).HasConversion(
                    ownerId => ownerId.Value,
                    ownerId => new PersonId(ownerId));

                mib.Property(mi => mi.AssigneeId).HasConversion(
                    assigneeId => assigneeId.Value,
                    assigneeId => new PersonId(assigneeId));

                mib.HasOne<Person>()
                    .WithMany()
                    .HasForeignKey(mi => mi.OwnerId)
                    .HasPrincipalKey(m => m.PersonId)
                    .IsRequired();

                mib.HasOne<Person>()
                    .WithMany()
                    .HasForeignKey(mi => mi.AssigneeId)
                    .HasPrincipalKey(m => m.PersonId)
                    .IsRequired();
            });

            builder.OwnsOne(t => t.Estimation, estimationBuilder =>
            {
                estimationBuilder.Property(est => est.EstimatedStartDateTime).IsRequired();
                estimationBuilder.Property(est => est.EstimatedEndDateTime).IsRequired();
                estimationBuilder.Ignore(est => est.EstimatedWorkDuration);
            });

            builder
                .HasOne(t => t.Stats)
                .WithOne(ts => ts.Task)
                .HasForeignKey<TaskStatistic>(ts => ts.TaskId)
                .HasPrincipalKey<TodoTask>(t => t.TaskId);

            builder.Ignore(t => t.Flags);
            builder.Ignore(t => t.State);

            builder.Property(t => t.Status).HasMaxLength(50)
                .HasConversion(status => status.ToString(),
                    status => (TodoTaskStatus)Enum.Parse(typeof(TodoTaskStatus), status));
        }
    }
}
