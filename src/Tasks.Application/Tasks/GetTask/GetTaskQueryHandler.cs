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
            var task = await _taskQueriesRepository.GetAsync(request.TaskId.Value, cancellationToken);

            if (task is null)
            {
                return Result.Failure<GetTaskResponse>(Error.NotFound(
                    "GetTask.NotFound",
                    $"Task with specified id {request.TaskId} was not found"));
            }

            return Result.Success(new GetTaskResponse(
                task.Id,
                task.TaskId,
                task.IsDeleted,
                task.MainInfo,
                task.MainInfo.OwnerId,
                task.MainInfo.AssigneeId,
                task.Estimation,
                task.Status,
                task.Stats));
        }
    }
}
