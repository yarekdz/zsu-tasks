using Tasks.Domain.Tasks;

namespace Tasks.Domain.Abstractions.Repositories.Commands
{
    public interface ITaskCommandsRepository : ICommandsRepository<TodoTask>
    {
        Task DeleteTaskAsync(Guid id, CancellationToken ct);
        Task DeleteTasksAsync(Guid[] id, CancellationToken ct);
    }
}
