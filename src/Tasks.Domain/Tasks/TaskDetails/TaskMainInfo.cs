using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks.TaskDetails
{
    public class TaskMainInfo
    {
        public TaskMainInfo(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public string? Description { get; set; }

        public Category Category { get; set; } = Category.Default;

        public Priority Priority { get; set; } = Priority.Create();

    }
}
