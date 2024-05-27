namespace Tasks.Domain.TaskDetails
{
    public class TaskDependency
    {
        public TodoTask? DependOnTodoTask { get; set; }
        public string OtherDependency { get; set; } = string.Empty;
    }
}
