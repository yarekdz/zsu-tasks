using MediatR;
using Tasks.Application.Abstractions.Messaging;
using Tasks.Application.AuditLogs.Add;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.Update
{
    internal class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;
        private readonly IMediator _mediator;

        public UpdateTaskCommandHandler(
            ITaskQueriesRepository taskQueriesRepository, 
            ITaskCommandsRepository taskCommandsRepository, 
            IMediator mediator)
        {
            _taskQueriesRepository = taskQueriesRepository;
            _taskCommandsRepository = taskCommandsRepository;
            _mediator = mediator;
        }

        public async Task<Result> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            if (command.TaskId is null)
            {
                return Result.Failure(TaskErrors.TaskIdIsInvalid);
            }

            var task = await _taskQueriesRepository.GetAsync(command.TaskId.Value, cancellationToken);

            if (task == null)
            {
                return Result.Failure(TaskErrors.TaskNotFound);
            }

            task.SetTitle(command.Title ?? task.MainInfo.Title);
            task.SetDescription(command.Description ?? task.MainInfo.Description);
            task.SetPriority(command.Priority ?? task.MainInfo.Priority);
            task.SetAssignee(command.AssigneeId ?? task.MainInfo.AssigneeId);

            await _taskCommandsRepository.UpdateAsync(task, cancellationToken);

            await _mediator.Send(new AddAuditLogCommand(
                AuditLogAction.TaskUpdate,
                "System",
                new[] { task.MainInfo.Title }), cancellationToken);

            return Result.Success();
        }
    }
}
