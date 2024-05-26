using Tasks.Domain.Tasks;

namespace Tasks.Domain.StateTransitions;

public interface IStateTransition
{
    IStateTransition SetNext(IStateTransition stateTransition);
    IStateTransition SetPrev(IStateTransition stateTransition);

    TodoTask HandleNextState(TodoTask task);
    TodoTask HandlePrevState(TodoTask task);

}