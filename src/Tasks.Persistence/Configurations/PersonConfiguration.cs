using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Person;

namespace Tasks.Persistence.Configurations
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(t => t.PersonId).HasConversion(
                personId => personId.Value,
                value => new PersonId(value));

            builder.Property(p => p.Email).HasMaxLength(255).IsRequired();

            builder.HasIndex(p => p.PersonId).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();
        }
    }
}
