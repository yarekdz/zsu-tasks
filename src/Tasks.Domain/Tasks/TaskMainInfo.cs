using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks
{
    public class TaskMainInfo
    {
        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public Category Category { get; private set; } = new Category();

        public Priority Priority { get; private set; } = Priority.Create();


    }
}
