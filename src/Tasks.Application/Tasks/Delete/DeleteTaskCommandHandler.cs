using MediatR;
using Tasks.Application.Abstractions.Messaging;
using Tasks.Application.AuditLogs.Add;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.Delete
{
    internal class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;
        private readonly IMediator _mediator;

        public DeleteTaskCommandHandler(
            ITaskQueriesRepository taskQueriesRepository,
            ITaskCommandsRepository taskCommandsRepository, 
            IMediator mediator)
        {
            _taskCommandsRepository = taskCommandsRepository;
            _mediator = mediator;
            _taskQueriesRepository = taskQueriesRepository;
        }

        public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskQueriesRepository.GetAsync(request.TaskId.Value, cancellationToken);

            if (task == null)
            {
                return Result.Failure(TaskErrors.TaskNotFound);
            }

            await _taskCommandsRepository.DeleteTaskAsync(request.TaskId.Value, cancellationToken);

            await _mediator.Send(new AddAuditLogCommand(
                AuditLogAction.TaskDelete,
                "System",
                new[] { task.MainInfo.Title }), cancellationToken);

            return Result.Success();
        }
    }
}
