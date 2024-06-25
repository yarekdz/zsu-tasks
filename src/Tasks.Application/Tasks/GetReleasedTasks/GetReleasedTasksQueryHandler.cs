using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Shared;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.GetReleasedTasks
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
                    TaskCategory.Create(t.Category),
                    Priority.Create(t.Priority),
                    (TodoTaskStatus)t.Status)));
        }
    }
}