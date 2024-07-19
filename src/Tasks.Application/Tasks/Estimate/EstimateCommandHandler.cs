using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Estimate
{
    internal class EstimateCommandHandler : ICommandHandler<EstimateCommand, EstimateCommandResponse>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public EstimateCommandHandler(
            ITaskCommandsRepository taskCommandsRepository, 
            ITaskQueriesRepository taskQueriesRepository)
        {
            _taskCommandsRepository = taskCommandsRepository;
            _taskQueriesRepository = taskQueriesRepository;
        }

        public async Task<Result<EstimateCommandResponse>> Handle(EstimateCommand command,
            CancellationToken cancellationToken)
        {
            if (command.TaskId is null)
            {
                return Result.Failure<EstimateCommandResponse>(TaskErrors.TaskIdIsInvalid);
            }

            var task = await _taskQueriesRepository.GetAsync(command.TaskId.Value, cancellationToken);

            if (task == null)
            {
                return Result.Failure<EstimateCommandResponse>(TaskErrors.TaskNotFound);
            }

            task.Estimate(new TaskEstimation
            {
                EstimatedStartDateTime = command.EstimatedStartDateTime,
                EstimatedEndDateTime = command.EstimatedEndDateTime
            });

            if (task.Estimation?.EstimatedWorkDuration == null)
            {
                return Result.Failure<EstimateCommandResponse>(TaskErrors.Estimate.InvalidDuration);
            }

            await _taskCommandsRepository.UpdateAsync(task, cancellationToken);

            return Result.Success(new EstimateCommandResponse(task.Estimation.EstimatedWorkDuration));
        }
    }
}
