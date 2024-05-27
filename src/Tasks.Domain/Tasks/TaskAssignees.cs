namespace Tasks.Domain.Tasks;

public class TaskAssignees
{
    public Person TaskOwner { get; private set; }
    public Person Assignee { get; private set; }
}