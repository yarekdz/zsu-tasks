using Tasks.Domain.Person;

namespace Tasks.Domain.Tasks.TaskDetails;

public class TaskAssignees
{
    public TaskAssignees(PersonId ownerId, PersonId assigneeId)
    {
        OwnerId = ownerId;
        AssigneeId = assigneeId;
    }

    public PersonId OwnerId { get; set; }
    public Person.Person? Owner { get; set; }

    public PersonId AssigneeId { get; set; }
    public Person.Person? Assignee { get; set; }

    //public Person[] Followers { get; set; }
}