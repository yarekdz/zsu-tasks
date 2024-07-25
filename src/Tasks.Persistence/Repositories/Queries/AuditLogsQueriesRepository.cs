using Microsoft.EntityFrameworkCore;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.AuditLog;

namespace Tasks.Persistence.Repositories.Queries
{
    internal sealed class AuditLogsQueriesRepository : IAuditLogsQueriesRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public AuditLogsQueriesRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuditLog?> GetAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.AuditLogs
                .SingleOrDefaultAsync(l => l.Id == id, ct);
        }
    }
}
