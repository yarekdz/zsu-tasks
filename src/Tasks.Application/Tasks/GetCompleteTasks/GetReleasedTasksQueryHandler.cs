using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetCompleteTasks
{
    internal sealed class
        GetReleasedTasksQueryHandler : IQueryHandler<GetReleasedTasksQuery, IEnumerable<GetReleasedTasksQueryResponse>>
    {
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public GetReleasedTasksQueryHandler(ITaskQueriesRepository taskQueriesRepository)
        {
            _taskQueriesRepository = taskQueriesRepository;
        }

        public async Task<Result<IEnumerable<GetReleasedTasksQueryResponse>>> Handle(
            GetReleasedTasksQuery request,
            CancellationToken cancellationToken)
        {
            var completedTasks = await _taskQueriesRepository.GetReleasedTasksAsync(cancellationToken);

            return Result.Success(completedTasks.Select(t =>
                new GetReleasedTasksQueryResponse(
                    t.Id,
                    new TaskId(t.TaskId),
                    t.Title,
                    t.Description,
                    TaskCategory.FromName(t.Category))));
        }
    }
}