using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks
{
    public class ComplexTodoTask : TodoTask
    {
        public IReadOnlyList<TodoTask> BreakDownTasks { get; private set; }

        public CompletionRate CompletionRate { get; private set; }

        public IReadOnlyList<Person> Assignees { get; private set; }
    }
}
