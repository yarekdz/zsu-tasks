using Tasks.Domain.Abstractions.Dtos.Tasks;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.Abstractions.Repositories.Queries
{
    public interface ITaskQueriesRepository : IQueriesRepository<TodoTask>
    {
        Task<TaskSummary?> GetMainInfoByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
