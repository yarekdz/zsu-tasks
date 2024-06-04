﻿using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Domain.States
{
    public interface ITaskState
    {
        string Title { get; }
        TodoTaskStatus Status { get; }
        Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo);
        Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation);

        #region v2

        //todo v2
        //Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency);

        #endregion

        Result<TodoTask> StartWork(TodoTask task);
        Result<TodoTask> CompleteWork(TodoTask task);
        Result<TodoTask> Verify(TodoTask task);
        Result<TodoTask> Approve(TodoTask task);
        Result<TodoTask> Release(TodoTask task);
        Result<TodoTask> Terminate(TodoTask task);
    }
}
