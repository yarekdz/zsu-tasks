using Tasks.Domain.Tasks;

namespace Tasks.Domain.StateTransitions
{
    //In automata theory and sequential logic,
    //a state-transition table is a table showing what state a finite-state machine will move to,
    //based on the current state and other inputs.

    public abstract class StateTransition : IStateTransition
    {
        private IStateTransition _nextStateTransition;
        private IStateTransition _prevStateTransition;

        public IStateTransition SetNext(IStateTransition stateTransition)
        {
            this._nextStateTransition = stateTransition;

            return stateTransition;
        }

        public IStateTransition SetPrev(IStateTransition stateTransition)
        {
            this._prevStateTransition = stateTransition;

            return stateTransition;
        }

        public abstract TodoTask HandleNextState(TodoTask task);

        public abstract TodoTask HandlePrevState(TodoTask task);
        //todo: notification
    }
}
