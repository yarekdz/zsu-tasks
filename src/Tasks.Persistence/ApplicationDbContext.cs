using Microsoft.EntityFrameworkCore;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Person;
using Tasks.Domain.Tasks;

namespace Tasks.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        public DbSet<TodoTask> Tasks { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Person> Persons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
