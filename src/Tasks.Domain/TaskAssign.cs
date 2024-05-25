namespace Tasks.Domain;

public class TaskAssign
{
    public Person TaskOwner { get; private set; }
    public Person Assignee { get; private set; }
}