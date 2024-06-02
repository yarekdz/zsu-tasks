using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.DomainErrors;

namespace Tasks.Domain.States
{
    public class CreatedState : ITaskState
    {
        public string Title => "Task Created state";
        public TodoTaskStatus Status => TodoTaskStatus.Created;

        public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees)
        {
            if (assignees.Assignee == null)
            {
                return Result.Failure<TodoTask>(TaskErrors.Assignee.InvalidAssignee);
            }

            if (assignees.Owner == null)
            {
                return Result.Failure<TodoTask>(TaskErrors.Assignee.InvalidOwner);
            }

            //todo: more domain errors to validate

            task.SetState(new AssignedState());

            return Result.Success(task);
        }

        public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo)
        {
            return Result.Failure<TodoTask>(TaskErrors.Create.TaskIsAlreadyCreated);
        }

        public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> StartWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> CompleteWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> Verify(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> Approve(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> Release(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
        public Result<TodoTask> Terminate(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Assignee.CanNotPerformActionNotAssignedTask);
    }
}
