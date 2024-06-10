using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Create
{
    internal class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;

        public CreateTaskCommandHandler(
            ITaskCommandsRepository taskCommandsRepository)
        {
            _taskCommandsRepository = taskCommandsRepository;
        }


        public async Task<Result> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
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

            return Result.Success();
        }
    }
}
