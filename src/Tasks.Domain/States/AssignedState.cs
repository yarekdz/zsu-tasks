using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.States;

public class AssignedState : ITaskState
{
    public string Title => "Assign";

    public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation)
    {
        if (estimation.Duration == null)
        {
            return Result.Failure<TodoTask>(DomainErrors.TaskErrors.InvalidEstimatedDuration);
        }

        //todo: more domain errors to validate

        task.SetEstimation(estimation);

        task.SetStatus(TodoTaskStatus.Estimated);
        //todo:
        //task.SetState(new EstimatedState());

        return Result.Success(task);
    }

    public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo)
    {
        throw new InvalidOperationException("Task is already assigned.");
    }

    public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees)
    {
        throw new InvalidOperationException("Task is already assigned.");
    }

   

    public Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency)
    {
        throw new InvalidOperationException("Cannot add dependency to a task that is not estimated.");
    }

    public Result<TodoTask> StartWork(TodoTask task)
    {
        throw new InvalidOperationException("Cannot start work on a task that is not estimated.");
    }

    public Result<TodoTask> CompleteWork(TodoTask task)
    {
        throw new InvalidOperationException("Cannot complete work on a task that is not estimated.");
    }

    public Result<TodoTask> Verify(TodoTask task)
    {
        throw new InvalidOperationException("Cannot verify a task that is not estimated.");
    }

    public Result<TodoTask> Approve(TodoTask task)
    {
        throw new InvalidOperationException("Cannot approve a task that is not estimated.");
    }

    public Result<TodoTask> Release(TodoTask task)
    {
        throw new InvalidOperationException("Cannot release a task that is not estimated.");
    }

    public Result<TodoTask> Terminate(TodoTask task)
    {
        throw new InvalidOperationException("Cannot terminate a task that is not estimated.");
    }
}