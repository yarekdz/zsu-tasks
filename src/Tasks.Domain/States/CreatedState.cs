using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.States
{
    public class CreatedState : ITaskState
    {
        public string Title => "Create";

        public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees)
        {
            if (assignees.Assignee == null)
            {
                return Result.Failure<TodoTask>(DomainErrors.TaskErrors.InvalidAssignee);
            }

            //todo: more domain errors to validate

            task.SetAssignees(assignees);

            task.SetStatus(TodoTaskStatus.Assigned);
            task.SetState(new AssignedState());

            return Result.Success(task);
        }

        public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo)
        {
            throw new InvalidOperationException("Task is already created.");
        }

        public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => throw new InvalidOperationException("Cannot estimate a task that is not assigned.");
        public Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency) => throw new InvalidOperationException("Cannot schedule a task that is not assigned.");
        public Result<TodoTask> StartWork(TodoTask task) => throw new InvalidOperationException("Cannot start work on a task that is not assigned.");
        public Result<TodoTask> CompleteWork(TodoTask task) => throw new InvalidOperationException("Cannot complete work on a task that is not assigned.");
        public Result<TodoTask> Verify(TodoTask task) => throw new InvalidOperationException("Cannot verify a task that is not assigned.");
        public Result<TodoTask> Approve(TodoTask task) => throw new InvalidOperationException("Cannot approve a task that is not assigned.");
        public Result<TodoTask> Release(TodoTask task) => throw new InvalidOperationException("Cannot release a task that is not assigned.");
        public Result<TodoTask> Terminate(TodoTask task) => throw new InvalidOperationException("Cannot terminate a task that is not assigned.");
    }
}
