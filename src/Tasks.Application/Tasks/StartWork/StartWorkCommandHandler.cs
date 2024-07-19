using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.StartWork
{
    internal class StartWorkCommandHandler : ICommandHandler<StartWorkCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public StartWorkCommandHandler(
            ITaskCommandsRepository taskCommandsRepository, 
            ITaskQueriesRepository taskQueriesRepository)
        {
            _taskCommandsRepository = taskCommandsRepository;
            _taskQueriesRepository = taskQueriesRepository;
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

            return Result.Success();
        }
    }
}
