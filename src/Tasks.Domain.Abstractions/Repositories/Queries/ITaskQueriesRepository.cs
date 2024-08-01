using Tasks.Domain.Abstractions.Dtos.Tasks;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.Abstractions.Repositories.Queries
{
    public interface ITaskQueriesRepository : IQueriesRepository<TodoTask>
    {
        Task<TaskListDto[]> GetAllAsync(CancellationToken ct);
        Task<IEnumerable<TaskListDto>> GetReleasedTasksAsync(CancellationToken ct);
        Task<IEnumerable<AuditLog.AuditLog>> GetAuditLogAsync(Guid taskId, CancellationToken ct);
    }
}
