using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetAllTasks
{
    internal sealed class GetAllTasksQueryHandler : IQueryHandler<GetAllTasksQuery, GetAllTasksResponse[]>
    {
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public GetAllTasksQueryHandler(ITaskQueriesRepository taskQueriesRepository)
        {
            _taskQueriesRepository = taskQueriesRepository;
        }

        public async Task<Result<GetAllTasksResponse[]>> Handle(
            GetAllTasksQuery request,
            CancellationToken cancellationToken)
        {
            var allTasks = await _taskQueriesRepository.GetAllAsync(cancellationToken);

            return Result.Success(allTasks.Select(t =>
                    new GetAllTasksResponse(
                        t.Id,
                        new TaskId(t.TaskId),
                        t.Title,
                        t.Description,
                        TaskCategory.Create(t.Category)))
                .ToArray());
        }
    }
}