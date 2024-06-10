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

        public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskQueriesRepository.Get(request.TaskId.Value, cancellationToken);

            if (task == null)
            {
                return Result.Failure(TaskErrors.TaskNotFound);
            }

            await _taskCommandsRepository.UpdateAsync(task, cancellationToken);

            return Result.Success();
        }
    }
}
