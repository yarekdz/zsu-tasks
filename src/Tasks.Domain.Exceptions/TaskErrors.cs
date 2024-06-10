using Tasks.Domain.Shared;

namespace Tasks.Domain.Errors
{
    public static class TaskErrors
    {
        public static Error TaskNotFound = Error.NotFound("TaskErrors.TaskNotFound",
            "Task not found");

        public static class Create
        {
            public static readonly Error NullTask = Error.Failure("CreateTask.NullTask",
                "Task could not bew created.");

            public static readonly Error InvalidTitle = Error.Validation("CreateTask.InvalidTitle",
                "Can't create new Task with invalid title.");

            public static readonly Error InvalidDescription = Error.Validation("CreateTask.InvalidDescription",
                "Can't create new Task with invalid description.");

            public static Error InvalidPriorityForHighRiskyCategory = Error.Validation("CreateTask.InvalidPriorityForHighRiskyCategory",
                "For HighRisky tasks priority should be Highest");

            public static Error CanNotPerformActionNotCreatedTask = Error.Conflict("CreateTask.CanNotPerformActionNotCreatedTask",
                "Action is not allowed for a task that is not created.");

            public static Error TaskIsAlreadyCreated = Error.Conflict("CreateTask.TaskIsAlreadyCreated",
                "Task is already created.");
        }

        public static class Assignee
        {
            public static Error InvalidAssignee = Error.Validation("AssignTask.InvalidAssignee",
                "Can't assign this person to the Task.");

            public static Error InvalidOwner = Error.Validation("AssignTask.InvalidOwner",
                "Can't assign this owner to the Task.");

            public static Error CanNotPerformActionNotAssignedTask = Error.Conflict("AssignTask.CanNotPerformActionNotAssignedTask",
                "Action is not allowed for a task that is not assigned.");

            public static Error TaskIsAlreadyAssigned = Error.Conflict("AssignTask.TaskIsAlreadyAssigned",
                "Task is already assigned.");

            public static Error CouldNotChangeOwner = Error.Conflict("AssignTask.CouldNotChangeOwner",
                "Could not change Owner.");
        }

        public static class Estimate
        {
            public static Error InvalidDuration = Error.Validation("EstimateTask.InvalidDuration",
                "Can't set those dates to the Task duration.");

            public static Error StartDateCouldNotBeGreaterThanEndDate = Error.Validation(
                "EstimateTask.StartDateCouldNotBeGreaterThanStartDate",
                "StartDate could not be greater than StartDate.");

            public static Error CanNotPerformActionNotEstimatedTask = Error.Conflict("EstimateTask.CanNotPerformActionNotEstimatedTask",
                "Action is not allowed for a task that is not estimated.");

            public static Error InvalidRate = Error.Validation("EstimateTask.InvalidRate",
                "Rate must be between 0 and 100.");

            public static Error TaskIsAlreadyEstimated = Error.Conflict("EstimateTask.TaskIsAlreadyEstimated",
                "Task is already estimated.");

        }

        public static class WorkStart
        {
            public static Error TaskIsAlreadyStarted = Error.Conflict("WorkStart.TaskIsAlreadyStarted",
                "Task is already started to work.");

            public static Error CanNotPerformActionNotStartedWorkTask = Error.Conflict("WorkStart.CanNotPerformActionNotStartedWorkTask",
                "Action is not allowed for a task that is not started to work.");
        }

        public static class WorkComplete
        {
            public static Error TaskIsAlreadyCompleted = Error.Conflict("WorkComplete.TaskIsAlreadyCompleted",
                "Task is already completed.");

            public static Error CanNotPerformActionNotCompletedTask = Error.Conflict("WorkComplete.CanNotPerformActionNotCompletedTask",
                "Action is not allowed for a task that is not completed.");
        }

        public static class Verified
        {
            public static Error TaskIsAlreadyVerified = Error.Conflict("WorkComplete.TaskIsAlreadyVerified",
                "Task is already verified.");

            public static Error CanNotPerformActionNotVerifiedTask = Error.Conflict("WorkComplete.CanNotPerformActionNotVerifiedTask",
                "Action is not allowed for a task that is not completed.");
        }

    }
}
