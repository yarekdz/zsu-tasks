using Tasks.Domain.States;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Domain.Tasks
{
    public class RecurringTodoTask : TodoTask
    {
        protected RecurringTodoTask(Guid id, TaskMainInfo mainInfo, ITaskState initState) : base(id, mainInfo, initState)
        {
        }
    }
}
