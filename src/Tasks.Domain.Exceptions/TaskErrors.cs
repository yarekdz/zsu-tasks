using Tasks.Domain.Shared;

namespace Tasks.DomainErrors
{
    public static class TaskErrors
    {
        public static readonly Error NullTask = new("CreateTask.NullTask",
            "Task could not bew created.");

        public static readonly Error InvalidDescription = new("CreateTask.InvalidDescription",
            "Can't create new Task with invalid description.");
    }
}
