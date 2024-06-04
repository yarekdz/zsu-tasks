namespace Tasks.Domain.Events.Tasks
{
    public sealed record WorkStartedDomainEvent(Guid TaskId) : IDomainEvent;
}
