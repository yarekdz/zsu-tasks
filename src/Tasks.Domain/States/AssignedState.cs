using Tasks.Domain.Shared;
using Tasks.Domain.TaskDetails;
using Tasks.DomainErrors;

namespace Tasks.Domain.States;

public class AssignedState : ITaskState
{
    public string Title => "Assign";

    public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation)
    {
        if (estimation.StartDateTime > estimation.EndDateTime)
        {
            return Result.Failure<TodoTask>(TaskErrors.Estimate.StartDateCouldNotBeGreaterThanStartDate);
        }

        //todo: more domain errors to validate

        task.SetEstimation(estimation);

        task.SetStatus(TodoTaskStatus.Estimated);
        //todo:
        //task.SetState(new EstimatedState());

        return Result.Success(task);
    }

    public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo) => Result.Failure<TodoTask>(TaskErrors.Assigne.TaskIsAlreadyAssigned);
    public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees) => Result.Failure<TodoTask>(TaskErrors.Assigne.TaskIsAlreadyAssigned);
    public Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
    public Result<TodoTask> StartWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
    public Result<TodoTask> CompleteWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
    public Result<TodoTask> Verify(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
    public Result<TodoTask> Approve(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
    public Result<TodoTask> Release(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
    public Result<TodoTask> Terminate(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Estimate.CanNotPerformActionNotEstimatedTask);
}