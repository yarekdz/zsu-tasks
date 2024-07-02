using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Persistence.Repositories.Commands
{
    internal sealed class TaskCommandsRepository :
        CommandsBaseRepository<TodoTask>,
        ITaskCommandsRepository
    {
        public TaskCommandsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteTaskAsync(Guid id, CancellationToken ct)
        {
            // Fetch and mark the associated TaskStatistic entity as deleted
            var taskId = new TaskId(id);

            var taskStatistic = await Context.Set<TaskStatistic>().FirstOrDefaultAsync(ts => ts.TaskId == taskId, ct);
            if (taskStatistic != null)
            {
                taskStatistic.IsDeleted = true;
            }

            await DeleteAsync(id, ct);
        }

        public async Task DeleteTasksAsync(Guid[] ids, CancellationToken ct)
        {
            // Fetch and mark the associated TaskStatistic entity as deleted
            var taskIds = ids.Select(id => new TaskId(id));
            var taskStatistic = await Context.Set<TaskStatistic>().FirstOrDefaultAsync(ts => taskIds.Contains(ts.TaskId), ct);
            if (taskStatistic != null)
            {
                taskStatistic.IsDeleted = true;
            }

            await DeleteAsync(ids, ct);
        }
    }
}
