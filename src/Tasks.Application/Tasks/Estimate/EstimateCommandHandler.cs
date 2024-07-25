using MediatR;
using Tasks.Application.Abstractions.Messaging;
using Tasks.Application.AuditLogs.Add;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Estimate
{
    internal class EstimateCommandHandler : ICommandHandler<EstimateCommand, EstimateCommandResponse>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;
        private readonly IMediator _mediator;

        public EstimateCommandHandler(
            ITaskCommandsRepository taskCommandsRepository, 
            ITaskQueriesRepository taskQueriesRepository, 
            IMediator mediator)
        {
            _taskCommandsRepository = taskCommandsRepository;
            _taskQueriesRepository = taskQueriesRepository;
            _mediator = mediator;
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

            await _mediator.Send(new AddAuditLogCommand(
                AuditLogAction.TaskEstimate,
                "System",
                new[] { task.MainInfo.Title }), cancellationToken);

            return Result.Success(new EstimateCommandResponse(task.Estimation.EstimatedWorkDuration));
        }
    }
}
