using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.Update
{
    internal class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public UpdateTaskCommandHandler(
            ITaskQueriesRepository taskQueriesRepository, 
            ITaskCommandsRepository taskCommandsRepository)
        {
            _taskQueriesRepository = taskQueriesRepository;
            _taskCommandsRepository = taskCommandsRepository;
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

            return Result.Success();
        }
    }
}
