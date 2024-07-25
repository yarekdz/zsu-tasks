using MediatR;
using Tasks.Application.Abstractions.Messaging;
using Tasks.Application.AuditLogs.Add;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Shared;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Create
{
    internal class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, Guid>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly IMediator _mediator;

        public CreateTaskCommandHandler(
            ITaskCommandsRepository taskCommandsRepository, 
            IMediator mediator)
        {
            _taskCommandsRepository = taskCommandsRepository;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var defaultStateFactory = new DefaultStateFactory();

            var todoTask = TodoTask.Create(
                new TaskMainInfo(
                    command.Title,
                    command.OwnerId,
                    command.AssigneeId)
                {
                    Description = command.Description,
                    Category = command.Category,
                    Priority = command.Priority,
                },
                defaultStateFactory.StartSate);

            await _taskCommandsRepository.CreateAsync(todoTask, cancellationToken);

            await _mediator.Send(new AddAuditLogCommand(
                AuditLogAction.TaskCreate,
                "System",
                new[] { todoTask.MainInfo.Title }), cancellationToken);

            return todoTask.Id;
        }
    }
}
