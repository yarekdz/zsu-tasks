namespace Tasks.Domain.Tasks.TaskDetails
{
    public class TaskDependency
    {
        public TodoTask? DependOnTodoTask { get; set; }
        public string OtherDependency { get; set; } = string.Empty;
    }
}
