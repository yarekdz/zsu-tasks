using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.DomainErrors;

namespace Tasks.Domain.States;

public class WorkCompletedState : ITaskState
{
    public string Title => "Work Completed state";
    public TodoTaskStatus Status => TodoTaskStatus.WorkCompleted;

    public Result<TodoTask> Verify(TodoTask task)
    {
        //todo: implement
        return Result.Failure<TodoTask>(TaskErrors.WorkStart.TaskIsAlreadyStarted);
    }

    public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.TaskIsAlreadyCompleted);
    public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.TaskIsAlreadyCompleted);
    public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.TaskIsAlreadyCompleted);
    public Result<TodoTask> StartWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.TaskIsAlreadyCompleted);
    public Result<TodoTask> CompleteWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.TaskIsAlreadyCompleted);
    public Result<TodoTask> Approve(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Verified.CanNotPerformActionNotVerifiedTask);
    public Result<TodoTask> Release(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Verified.CanNotPerformActionNotVerifiedTask);
    public Result<TodoTask> Terminate(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Verified.CanNotPerformActionNotVerifiedTask);
}