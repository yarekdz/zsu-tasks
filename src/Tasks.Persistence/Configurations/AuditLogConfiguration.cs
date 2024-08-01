using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.AuditLog;

namespace Tasks.Persistence.Configurations
{
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(al => al.Id);

            builder.Property(p => p.Action).HasMaxLength(55).IsRequired();
            builder.Property(p => p.Message).HasMaxLength(255).IsRequired();
            builder.Property(p => p.ObjectRelateId).IsRequired();
        }
    }
}
