using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.Errors;

namespace Tasks.Domain.States
{
    public class CreatedState : ITaskState
    {
        public string Title => "Task Created state";
        public TodoTaskStatus Status => TodoTaskStatus.Created;

        public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees)
        {
            if (string.IsNullOrEmpty(assignees.Assignee?.Email))
            {
                return Result.Failure<TodoTask>(TaskErrors.Assignee.InvalidAssignee);
            }

            if (string.IsNullOrEmpty(assignees.Owner?.Email))
            {
                return Result.Failure<TodoTask>(TaskErrors.Assignee.InvalidOwner);
            }

            if (task.Assignees?.Owner != null &&
                !string.Equals(task.Assignees?.Owner?.Email, assignees.Owner.Email))
            {
                return Result.Failure<TodoTask>(TaskErrors.Assignee.CouldNotChangeOwner);
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
