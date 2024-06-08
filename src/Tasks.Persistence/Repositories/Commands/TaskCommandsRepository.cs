using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Tasks;

namespace Tasks.Persistence.Repositories.Commands
{
    internal sealed class TaskCommandsRepository : ITaskCommandsRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public TaskCommandsRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreateAsync(TodoTask domainModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id, bool ignoreDependencies = false)
        {
            throw new NotImplementedException();
            //todo: on UPDATE, cache.Remove($"tasks-{id}"(

        }

        public Task DeleteAsync(string[] ids, bool ignoreDependencies = false)

        {
            throw new NotImplementedException();
            //todo: on UPDATE, cache.Remove($"tasks-{id}"(

        }

        //todo: on UPDATE, cache.Remove($"tasks-{id}"(
    }
}
