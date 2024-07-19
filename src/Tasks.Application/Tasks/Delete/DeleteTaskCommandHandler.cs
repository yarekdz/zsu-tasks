using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.Delete
{
    internal class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
    {
        private readonly ITaskCommandsRepository _taskCommandsRepository;
        private readonly ITaskQueriesRepository _taskQueriesRepository;

        public DeleteTaskCommandHandler(
            ITaskQueriesRepository taskQueriesRepository,
            ITaskCommandsRepository taskCommandsRepository)
        {
            _taskCommandsRepository = taskCommandsRepository;
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

            return Result.Success();
        }
    }
}
