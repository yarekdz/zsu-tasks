﻿using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks.TaskDetails;

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
            var taskSummary = await _taskQueriesRepository.GetMainInfoByIdAsync(request.TaskId.Value, cancellationToken);

            if (taskSummary is null)
            {
                return Result.Failure<GetTaskResponse>(Error.NotFound(
                    "GetTask.NotFound",
                    $"Task with specified id {request.TaskId} was not found"));
            }

            return Result.Success(new GetTaskResponse(
                taskSummary.Id,
                new TaskId(taskSummary.TaskId),
                taskSummary.Title,
                taskSummary.Description,
                TaskCategory.FromName(taskSummary.Category)));
        }
    }
}
