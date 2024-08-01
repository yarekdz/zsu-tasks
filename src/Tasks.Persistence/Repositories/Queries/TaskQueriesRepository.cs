using Microsoft.EntityFrameworkCore;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Dtos.Tasks;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.AuditLog;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Persistence.Repositories.Queries
{
    internal sealed class TaskQueriesRepository : ITaskQueriesRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly StateFactory _stateFactory;

        public TaskQueriesRepository(
            IApplicationDbContext dbContext, 
            StateFactory stateFactory)
        {
            _dbContext = dbContext;
            _stateFactory = stateFactory;
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
            var task = await _dbContext.Tasks
                .Include(t => t.Estimation)
                .Include(t => t.Stats)
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.TaskId == new TaskId(id), ct);

            task?.SetState(_stateFactory.GetStateBasedOnTaskStatus(task.Status));

            return task;
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

        public async Task<IEnumerable<AuditLog>> GetAuditLogAsync(Guid taskId, CancellationToken ct)
        {
            return await _dbContext
                .Database.SqlQuery<AuditLog>($@"
                    SELECT 
                        ""Id"",
                        ""Action"",
                        ""Message"",
                        ""CreatedAt"",
                        ""CreatedBy"",
                        ""IsDeleted"",
                        ""ObjectRelateId""
                    FROM 
                        PUBLIC.""AuditLogs"" a
                    WHERE
                        a.""ObjectRelateId"" = {taskId}
                    ORDER BY a.""CreatedAt"" ASC")
                .ToListAsync(ct);
        }
    }
}
