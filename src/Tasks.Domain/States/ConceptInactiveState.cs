using Tasks.Domain.Shared;
using Tasks.Domain.TaskDetails;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.States
{
    public class ConceptInactiveState : ITaskState
    {
        public string Title => "Init state";

        public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo)
        {
            if (string.IsNullOrEmpty(mainInfo.Description))
            {
                return Result.Failure<TodoTask>(DomainErrors.TaskErrors.InvalidDescription);
            }

            //todo: more domain errors to validate

            task.SetMainInfo(mainInfo);

            task.SetState(new CreatedState());
            task.SetStatus(TodoTaskStatus.Created);

            return Result.Success(task);
        }

        public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees) => throw new InvalidOperationException("Cannot assign a task that is not created.");
        public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => throw new InvalidOperationException("Cannot estimate a task that is not created.");
        public Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency) => throw new InvalidOperationException("Cannot add dependency to a task that is not created.");
        public Result<TodoTask> StartWork(TodoTask task) => throw new InvalidOperationException("Cannot start work on a task that is not created.");
        public Result<TodoTask> CompleteWork(TodoTask task) => throw new InvalidOperationException("Cannot complete work on a task that is not created.");
        public Result<TodoTask> Verify(TodoTask task) => throw new InvalidOperationException("Cannot verify a task that is not created.");
        public Result<TodoTask> Approve(TodoTask task) => throw new InvalidOperationException("Cannot approve a task that is not created.");
        public Result<TodoTask> Release(TodoTask task) => throw new InvalidOperationException("Cannot release a task that is not created.");
        public Result<TodoTask> Terminate(TodoTask task) => throw new InvalidOperationException("Cannot terminate a task that is not created.");
    }
}
