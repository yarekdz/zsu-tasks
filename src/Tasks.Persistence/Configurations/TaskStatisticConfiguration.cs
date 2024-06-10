using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Persistence.Configurations
{
    internal class TaskStatisticConfiguration : IEntityTypeConfiguration<TaskStatistic>
    {
        public void Configure(EntityTypeBuilder<TaskStatistic> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(t => t.TaskId)
                .HasConversion(taskId => taskId.Value,
                    value => new TaskId(value))
                .IsRequired();

            builder.Property(s => s.CreatedDate).IsRequired();
            builder.Ignore(s => s.ActualWorkDuration);
        }
    }
}
