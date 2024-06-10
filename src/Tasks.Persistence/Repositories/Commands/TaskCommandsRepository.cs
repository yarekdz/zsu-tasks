using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Tasks;

namespace Tasks.Persistence.Repositories.Commands
{
    internal sealed class TaskCommandsRepository :
        CommandsBaseRepository<TodoTask>,
        ITaskCommandsRepository
    {
        public TaskCommandsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
