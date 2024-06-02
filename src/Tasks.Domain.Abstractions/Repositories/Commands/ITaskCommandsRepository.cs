using Tasks.Domain.Tasks;

namespace Tasks.Domain.Abstractions.Repositories.Commands
{
    public interface ITaskCommandsRepository : ICommandsRepository<TodoTask>
    {
    }
}
