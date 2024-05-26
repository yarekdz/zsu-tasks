using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.States
{
    public abstract class State 
    {
        public abstract string Title { get; }
        public abstract Result DoStateWork();

        protected readonly TodoTask TodoTask;

        protected State(TodoTask todoTask)
        {
            TodoTask = todoTask;
        }
    }


    public class CompletedState : State
    {
    }

    public class VerifiedState : State
    {
    }

    public class VerifyState : State
    {
    }

    public class WorkCompletedState : State
    {
    }

    public class TestState : State
    {
    }

    public class WorkOnTaskState : State
    {
    }

    public class WorkStartedState : State
    {
    }

    public class ScheduleState : State
    {
    }

    public class EstimateState : State
    {
    }

    public class AssignState : State
    {
    }

    public class CustomState : State
    {
        public override string Title { get; protected set; }
        public override TodoTask DoStateWork(TodoTask task)
        {
            throw new NotImplementedException();
        }
    }
}
