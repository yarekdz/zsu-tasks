using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Create
{
    internal class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, Guid>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;

        public CreateTaskCommandHandler(
            ITaskCommandsRepository taskCommandsRepository)
        {
            _taskCommandsRepository = taskCommandsRepository;
        }


        public async Task<Result<Guid>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var todoTask = TodoTask.Create(
                new TaskMainInfo(
                    command.Title,
                    command.OwnerId,
                    command.AssigneeId)
                {
                    Description = command.Description,
                    Category = command.Category,
                    Priority = command.Priority,
                });

            await _taskCommandsRepository.CreateAsync(todoTask, cancellationToken);

            return todoTask.Id;
        }
    }
}
