using Tasks.Domain.Tasks;

namespace Tasks.Domain.StateTransitions;

public class EstimateTaskStateTransition : StateTransition
{
    public override TodoTask HandleNextState(TodoTask task)
    {
        throw new NotImplementedException();
    }

    public override TodoTask HandlePrevState(TodoTask task)
    {
        throw new NotImplementedException();
    }
}