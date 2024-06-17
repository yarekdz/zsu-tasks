using Tasks.Domain.Person;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks.TaskDetails
{
    public class TaskMainInfo
    {
        public TaskMainInfo(string title, PersonId ownerId, PersonId assigneeId)
        {
            Title = title;
            OwnerId = ownerId;
            AssigneeId = assigneeId;
        }

        public string Title { get; set; } //todo use Title domain type with validation

        public string? Description { get; set; }

        public TaskCategory Category { get; set; } = TaskCategory.Default;

        public Priority Priority { get; set; } = Priority.Create();

        public PersonId OwnerId { get; set; }
        public PersonId AssigneeId { get; set; }

    }
}
