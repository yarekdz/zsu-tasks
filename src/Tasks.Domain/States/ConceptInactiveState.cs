﻿using Tasks.Domain.Shared;
using Tasks.Domain.TaskDetails;
using Tasks.Domain.ValueObjects;
using Tasks.DomainErrors;

namespace Tasks.Domain.States
{
    public class ConceptInactiveState : ITaskState
    {
        public string Title => "Concept Init state";

        public Result<TodoTask> Create(TodoTask task, TaskMainInfo mainInfo)
        {
            if (string.IsNullOrEmpty(mainInfo.Title))
            {
                return Result.Failure<TodoTask>(TaskErrors.Create.InvalidTitle);
            }

            if (string.IsNullOrEmpty(mainInfo.Description))
            {
                return Result.Failure<TodoTask>(TaskErrors.Create.InvalidDescription);
            }

            //HighRisky category tasks should have  5 highest Priority
            if (mainInfo.Category is Category.HighRisky
                && mainInfo.Priority != Priority.HightestPriority)
            {
                return Result.Failure<TodoTask>(TaskErrors.Create.InvalidPriorityForHighRiskyCategory);
            }

            task.SetMainInfo(mainInfo);

            task.SetState(new CreatedState());
            task.SetStatus(TodoTaskStatus.Created);

            return Result.Success(task);
        }

        public Result<TodoTask> Assign(TodoTask task, TaskAssignees assignees) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> Estimate(TodoTask task, TaskEstimation estimation) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> AddDependencies(TodoTask task, TaskDependency dependency) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> StartWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> CompleteWork(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> Verify(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> Approve(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> Release(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
        public Result<TodoTask> Terminate(TodoTask task) => Result.Failure<TodoTask>(TaskErrors.Create.CanNotPerformActionNotCreatedTask);
    }
}
