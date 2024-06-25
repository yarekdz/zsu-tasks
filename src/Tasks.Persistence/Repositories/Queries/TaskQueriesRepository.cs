using Microsoft.EntityFrameworkCore;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Dtos.Tasks;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.States;
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

        public async Task<TaskListDto[]> GetAllAsync(CancellationToken ct)
        {
            return await _dbContext
                .Database
                .SqlQuery<TaskListDto>(@$"
                    SELECT 
                        ""Id"",
                        ""TaskId"",
                        ""MainInfo_Title"" AS ""Title"",
                        ""MainInfo_Description"" AS ""Description"",
                        ""MainInfo_Category"" AS ""Category"",
                        ""MainInfo_Priority"" AS ""Priority"",
                        ""Status"" AS ""Status""
                    FROM 
                        PUBLIC.""Tasks"" t
                    WHERE 
                       t.""IsDeleted"" = False ")
                .ToArrayAsync(ct);
        }

        public async Task<TodoTask?> GetAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.TaskId == new TaskId(id), ct);
        }

        public async Task<TaskSummary?> GetMainInfoByIdAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext
                .Database
                .SqlQuery<TaskSummary>($@"
                    SELECT *
                    FROM public.""Tasks"" t
                    WHERE t.""Id"" = {id}
                ")
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(ct);
        }

        public async Task<IEnumerable<TaskListDto>> GetReleasedTasksAsync(CancellationToken ct)
        {
            return await _dbContext
                .Database
                .SqlQuery<TaskListDto>(@$"
                   SELECT 
                        ""Id"",
                        ""TaskId"",
                        ""MainInfo_Title"" AS ""Title"",
                        ""MainInfo_Description"" AS ""Description"",
                        ""MainInfo_Category"" AS ""Category"",
                        ""MainInfo_Priority"" AS ""Priority"",
                        ""Status"" AS ""Status""
                    FROM 
                        PUBLIC.""Tasks"" t
                    WHERE 
                        t.""IsDeleted"" = False 
                        AND t.""Status"" = {TodoTaskStatus.Released}")
                .ToListAsync(ct);
        }
    }
}
