using Tasks.Domain.States;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.StateTransitions;

public class CreateTaskStateTransition : StateTransition
{
    public override TodoTask HandleNextState(TodoTask task)
    {
        if (task == null)
        {
            task = TodoTask.Create();
        }

        var createState = new CreateState(task);

        if (createState.DoStateWork().IsSuccess)
        {
            task.SetState(createState);
        }


        //todo: do more logic there

        return task;
    }

    public override TodoTask HandlePrevState(TodoTask task)
    {
        throw new NotImplementedException();
    }
}