using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.Errors;

namespace Tasks.Domain.States;

public class EstimatedState : ITaskState
{
    public string Title => "Task Estimated State";
    public TodoTaskStatus Status => TodoTaskStatus.Estimated;

    public Result<TodoTask> StartWork(TodoTask task)
    {


        task.SetState(new WorkStartedState());

        return Result.Success(task);
    }

    public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo) => Result.Failure<TodoTask>(TaskErrors.Estimate.TaskIsAlreadyEstimated);
    public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees) => Result.Failure<TodoTask>(TaskErrors.Estimate.TaskIsAlreadyEstimated);
    public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => Result.Failure<TodoTask>(TaskErrors.Estimate.TaskIsAlreadyEstimated);
    public Result<TodoTask> CompleteWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkStart.CanNotPerformActionNotStartedWorkTask);
    public Result<TodoTask> Verify(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkStart.CanNotPerformActionNotStartedWorkTask);
    public Result<TodoTask> Approve(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkStart.CanNotPerformActionNotStartedWorkTask);
    public Result<TodoTask> Release(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkStart.CanNotPerformActionNotStartedWorkTask);
    public Result<TodoTask> Terminate(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.WorkStart.CanNotPerformActionNotStartedWorkTask);
}