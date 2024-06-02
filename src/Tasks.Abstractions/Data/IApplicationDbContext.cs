using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Abstractions.Data
{
    public interface IApplicationDbContext //todo: split to read and write (read for anonymous types)
    {
        DbSet<TodoTask> Tasks { get; set; }

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
