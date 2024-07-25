using MediatR;
using Tasks.Application.Abstractions.Messaging;
using Tasks.Application.AuditLogs.Add;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.StartWork
{
    internal class StartWorkCommandHandler : ICommandHandler<StartWorkCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;
        private readonly IMediator _mediator;

        public StartWorkCommandHandler(
            ITaskCommandsRepository taskCommandsRepository, 
            ITaskQueriesRepository taskQueriesRepository, 
            IMediator mediator)
        {
            _taskCommandsRepository = taskCommandsRepository;
            _taskQueriesRepository = taskQueriesRepository;
            _mediator = mediator;
        }

        public async Task<Result> Handle(StartWorkCommand command, CancellationToken cancellationToken)
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

            task.StartWork();

            await _taskCommandsRepository.UpdateAsync(task, cancellationToken);

            await _mediator.Send(new AddAuditLogCommand(
                AuditLogAction.TaskStartWork,
                "System",
                new[] { task.MainInfo.Title }), cancellationToken);

            return Result.Success();
        }
    }
}
