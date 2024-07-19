using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Domain.States;

public class DefaultStateFactory : StateFactory
{
    public sealed override ITaskState StartSate { get; protected set; }
    public sealed override LinkedList<ITaskState> States { get; protected set; } = new();

    private readonly ConceptInactiveState _conceptInactiveState = new();
    private readonly CreatedState _createdState = new();
    private readonly EstimatedState _estimatedState = new();
    private readonly WorkStartedState _workStartedState = new();
    private readonly WorkCompletedState _workCompletedState = new();

    public DefaultStateFactory()
    {
        StartSate = _conceptInactiveState;

        States.AddFirst(StartSate);
        States.AddLast(_createdState);
        States.AddLast(_estimatedState);
        States.AddLast(_workStartedState);
        States.AddLast(_workCompletedState);
    }

    public override ITaskState GetStateBasedOnTaskStatus(TodoTaskStatus status)
    {
        return status switch
        {
            //todo: add missing statuses
            TodoTaskStatus.ConceptInactive => _conceptInactiveState,
            TodoTaskStatus.Created => _createdState,
            TodoTaskStatus.Estimated => _estimatedState,
            TodoTaskStatus.WorkStarted => _workStartedState,
            TodoTaskStatus.WorkCompleted => _workCompletedState,
            _ => throw new DomainInternalException(TaskErrors.DomainInternal.IncorrectStateFactorySetup)
        };
    }
}