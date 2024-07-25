using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.AuditLog;

namespace Tasks.Persistence.Repositories.Commands
{
    internal sealed class AuditLogsCommandsRepository : 
        CommandsBaseRepository<AuditLog>, 
        IAuditLogsCommandsRepository
    {
        public AuditLogsCommandsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
