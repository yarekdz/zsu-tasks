using Tasks.Domain.Shared;

namespace Tasks.DomainErrors
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

        public static class Assigne
        {
            public static Error InvalidAssignee = new("AssignTask.InvalidAssignee",
                "Can't assign this person to the Task.");

            public static Error CanNotPerformActionNotAssignedTask = new("CreateTask.CanNotPerformActionNotAssignedTask",
                "Action is not allowed for a task that is not assigned.");

            public static Error TaskIsAlreadyAssigned = new("CreateTask.TaskIsAlreadyAssigned",
                "Task is already assigned.");
        }

        public static class Estimate
        {
            public static Error InvalidEstimatedDuration = new("EstimateTask.InvalidEstimatedDuration",
                "Can't set this duration to the Task.");

            public static Error StartDateCouldNotBeGreaterThanStartDate = new(
                "EstimateTask.StartDateCouldNotBeGreaterThanStartDate",
                "StartDate could not be greater than StartDate.");

            public static Error CanNotPerformActionNotEstimatedTask = new("EstimateTask.CanNotPerformActionNotEstimatedTask",
                "Action is not allowed for a task that is not estimated.");
        }
    }
}
