using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.GetTask
{
    internal sealed class GetTaskQueryHandler : IQueryHandler<GetTaskQuery, GetTaskResponse>
    {
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public GetTaskQueryHandler(ITaskQueriesRepository taskQueriesRepository)
        {
            _taskQueriesRepository = taskQueriesRepository;
        }

        public async Task<Result<GetTaskResponse>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var taskSummary = await _taskQueriesRepository.GetMainInfoByIdAsync(request.TaskId, cancellationToken);

            if (taskSummary is null)
            {
                return Result.Failure<GetTaskResponse>(Error.NotFound(
                    (string)"GetTask.NotFound",
                    (string)$"Task with specified id {request.TaskId.ToString()} was not found"));
            }

            return Result.Success(new GetTaskResponse(
                taskSummary.TaskId,
                taskSummary.Title,
                taskSummary.Description,
                taskSummary.Category));
        }
    }
}
