using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Domain.Tasks
{
    public class RecurringTodoTask : TodoTask
    {
        protected RecurringTodoTask(Guid id, TaskMainInfo mainInfo) : base(id, mainInfo)
        {
        }
    }
}
