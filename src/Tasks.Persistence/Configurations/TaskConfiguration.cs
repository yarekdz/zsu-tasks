using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain;
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
                mainInfoBuilder.Property(mi => mi.Category).IsRequired().HasConversion(
                    c => c.ToString(),
                    c => (Category)Enum.Parse(typeof(Category), c));
                mainInfoBuilder.Property(mi => mi.Priority).IsRequired().HasConversion(
                    pr => pr.Value,
                    pr => Priority.Create(pr));
            });

            //todo: work with next columns and tables
        }
    }
}
