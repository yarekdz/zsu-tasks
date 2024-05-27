namespace Tasks.Domain.Tasks
{
    public class TaskDependency
    {
        public TodoTask DependOnTodoTask { get; private set; }
        public string OtherDependency { get; private set; }
    }
}
