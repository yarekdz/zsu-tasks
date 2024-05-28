using Tasks.Domain.Shared;
using Tasks.Domain.TaskDetails;
using Tasks.DomainErrors;

namespace Tasks.Domain.States;

public class WorkStartedState : ITaskState
{
    public string Title => "Work Started state";

    public Result<TodoTask> CompleteWork(TodoTask task)
    {

        task.SetStatus(TodoTaskStatus.WorkCompleted);
        task.SetState(new WorkCompletedState());

        return Result.Success(task);
    }

    public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo) => Result.Failure<TodoTask>(TaskErrors.WorkStart.TaskIsAlreadyStarted);
    public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees) => Result.Failure<TodoTask>(TaskErrors.WorkStart.TaskIsAlreadyStarted);
    public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => Result.Failure<TodoTask>(TaskErrors.WorkStart.TaskIsAlreadyStarted);
    public Result<TodoTask> StartWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkStart.TaskIsAlreadyStarted);
    public Result<TodoTask> Verify(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.CanNotPerformActionNotCompletedTask);
    public Result<TodoTask> Approve(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.CanNotPerformActionNotCompletedTask);
    public Result<TodoTask> Release(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.CanNotPerformActionNotCompletedTask);
    public Result<TodoTask> Terminate(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkComplete.CanNotPerformActionNotCompletedTask);
}