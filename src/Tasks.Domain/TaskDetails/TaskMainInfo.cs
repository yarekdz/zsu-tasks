using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.TaskDetails
{
    public class TaskMainInfo
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Category Category { get; set; } = new Category();

        public Priority Priority { get; set; } = Priority.Create();


    }
}
