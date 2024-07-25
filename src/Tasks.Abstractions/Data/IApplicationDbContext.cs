using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Person;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Abstractions.Data
{
    public interface IApplicationDbContext //todo: split to read and write (read for anonymous types)
    {
        DbSet<TodoTask> Tasks { get; set; }
        public DbSet<Person> Persons { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
