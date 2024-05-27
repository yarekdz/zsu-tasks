using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.States
{
    public interface ITaskState
    {
        public string Title { get; }
        Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo);
        Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees);
        Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation);
        Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency);
        Result<TodoTask> StartWork(TodoTask task);
        Result<TodoTask> CompleteWork(TodoTask task);
        Result<TodoTask> Verify(TodoTask task);
        Result<TodoTask> Approve(TodoTask task);
        Result<TodoTask> Release(TodoTask task);
        Result<TodoTask> Terminate(TodoTask task);
    }
}
