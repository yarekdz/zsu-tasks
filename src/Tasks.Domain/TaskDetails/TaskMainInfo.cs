using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.TaskDetails
{
    public class TaskMainInfo
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public Category? Category { get; set; }

        public Priority Priority { get; set; } = Priority.Create();


    }
}
