using Microsoft.EntityFrameworkCore;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Dtos.Tasks;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Persistence.Repositories.Queries
{
    internal sealed class TaskQueriesRepository : ITaskQueriesRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public TaskQueriesRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TodoTask>> GetAll(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoTask?> Get(Guid id, CancellationToken ct)
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == new TaskId(id), ct);
        }

        public async Task<TaskSummary?> GetMainInfoByIdAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext
                .Database
                .SqlQuery<TaskSummary?>(@$"
                    SELECT 
                        t.Id AS TaskId, 
                        t.Title AS Title, 
                        t.Description AS Description,
                        t.Category as Category
                    FROM Tasks AS t
                    WHERE t.Id = {id}")
                .SingleOrDefaultAsync(ct);
        }
    }
}
