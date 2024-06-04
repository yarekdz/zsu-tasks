using Tasks.Domain.Shared;

namespace Tasks.Domain.Errors
{
    public static class TaskErrors
    {
        public static class Create
        {
            public static readonly Error NullTask = new("CreateTask.NullTask",
                "Task could not bew created.");

            public static readonly Error InvalidTitle = new("CreateTask.InvalidTitle",
                "Can't create new Task with invalid title.");

            public static readonly Error InvalidDescription = new("CreateTask.InvalidDescription",
                "Can't create new Task with invalid description.");

            public static Error InvalidPriorityForHighRiskyCategory = new("CreateTask.InvalidPriorityForHighRiskyCategory",
                "For HighRisky tasks priority should be Highest");

            public static Error CanNotPerformActionNotCreatedTask = new("CreateTask.CanNotPerformActionNotCreatedTask",
                "Action is not allowed for a task that is not created.");

            public static Error TaskIsAlreadyCreated = new("CreateTask.TaskIsAlreadyCreated",
                "Task is already created.");
        }

        public static class Assignee
        {
            public static Error InvalidAssignee = new("AssignTask.InvalidAssignee",
                "Can't assign this person to the Task.");

            public static Error InvalidOwner = new("AssignTask.InvalidOwner",
                "Can't assign this owner to the Task.");

            public static Error CanNotPerformActionNotAssignedTask = new("AssignTask.CanNotPerformActionNotAssignedTask",
                "Action is not allowed for a task that is not assigned.");

            public static Error TaskIsAlreadyAssigned = new("AssignTask.TaskIsAlreadyAssigned",
                "Task is already assigned.");

            public static Error CouldNotChangeOwner = new("AssignTask.CouldNotChangeOwner",
                "Could not change Owner.");
        }

        public static class Estimate
        {
            public static Error InvalidDuration = new("EstimateTask.InvalidDuration",
                "Can't set those dates to the Task duration.");

            public static Error StartDateCouldNotBeGreaterThanEndDate = new(
                "EstimateTask.StartDateCouldNotBeGreaterThanStartDate",
                "StartDate could not be greater than StartDate.");

            public static Error CanNotPerformActionNotEstimatedTask = new("EstimateTask.CanNotPerformActionNotEstimatedTask",
                "Action is not allowed for a task that is not estimated.");

            public static Error InvalidRate = new("EstimateTask.InvalidRate",
                "Rate must be between 0 and 100.");

            public static Error TaskIsAlreadyEstimated = new("EstimateTask.TaskIsAlreadyEstimated",
                "Task is already estimated.");

        }

        public static class WorkStart
        {
            public static Error TaskIsAlreadyStarted = new("WorkStart.TaskIsAlreadyStarted",
                "Task is already started to work.");

            public static Error CanNotPerformActionNotStartedWorkTask = new("WorkStart.CanNotPerformActionNotStartedWorkTask",
                "Action is not allowed for a task that is not started to work.");
        }

        public static class WorkComplete
        {
            public static Error TaskIsAlreadyCompleted = new("WorkComplete.TaskIsAlreadyCompleted",
                "Task is already completed.");

            public static Error CanNotPerformActionNotCompletedTask = new("WorkComplete.CanNotPerformActionNotCompletedTask",
                "Action is not allowed for a task that is not completed.");
        }

        public static class Verified
        {
            public static Error TaskIsAlreadyVerified = new("WorkComplete.TaskIsAlreadyVerified",
                "Task is already verified.");

            public static Error CanNotPerformActionNotVerifiedTask = new("WorkComplete.CanNotPerformActionNotVerifiedTask",
                "Action is not allowed for a task that is not completed.");
        }

    }
}
