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

            builder.Property(t => t.Id).HasConversion(
                taskId => taskId.Value,
                value => new TaskId(value));

            builder.OwnsOne(t => t.MainInfo, mainInfoBuilder =>
            {
                mainInfoBuilder.Property(mi => mi.Title).IsRequired().HasMaxLength(100);

                mainInfoBuilder.Property(mi => mi.Description).HasMaxLength(500);

                mainInfoBuilder.Property(mi => mi.Category).IsRequired().HasMaxLength(50)
                    .HasConversion(c => c.ToString(),
                    c => (Category)Enum.Parse(typeof(Category), c));

                mainInfoBuilder.Property(mi => mi.Priority).IsRequired()
                    .HasConversion(pr => pr.Value,
                    pr => Priority.Create(pr));
            });

            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .IsRequired();

            builder.Property(a => a.OwnerId).HasConversion(
                ownerId => ownerId.Value,
                ownerId => new PersonId(ownerId));

            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .IsRequired();

            builder.Property(a => a.AssigneeId).HasConversion(
                assigneeId => assigneeId.Value,
                assigneeId => new PersonId(assigneeId));

            builder.OwnsOne(t => t.Estimation, estimationBuilder =>
            {
                estimationBuilder.Property(est => est.EstimatedStartDateTime).IsRequired();
                estimationBuilder.Property(est => est.EstimatedEndDateTime).IsRequired();
                estimationBuilder.Ignore(est => est.EstimatedWorkDuration);
            });

            builder.OwnsOne(t => t.Stats, statsBuilder =>
            {
                statsBuilder.WithOwner().HasForeignKey(statistic => statistic.TaskId);

                statsBuilder.Property(s => s.TaskId).HasConversion(
                    taskId => taskId.Value,
                    taskId => new TaskId(taskId));

                statsBuilder.Property(s => s.CreatedDate).IsRequired();
                statsBuilder.Ignore(s => s.ActualWorkDuration);
            });

            builder.Ignore(t => t.Flags);
            builder.Ignore(t => t.State);

            builder.Property(t => t.Status).HasMaxLength(50)
                .HasConversion(status => status.ToString(),
                status => (TodoTaskStatus)Enum.Parse(typeof(TodoTaskStatus), status));
        }
    }
}
