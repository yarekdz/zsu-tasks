namespace Tasks.Domain.States
{
    public abstract class StateFactory
    {
        public abstract ITaskState StartSate { get; protected set; }
        public abstract LinkedList<ITaskState> States { get; protected set; }
        public abstract ITaskState GetStateBasedOnTaskStatus(TodoTaskStatus  status);
    }
}
