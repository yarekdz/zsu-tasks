namespace Tasks.Domain.Events.Tasks
{
    public sealed record TaskCreatedDomainEvent(Guid TaskId) : IDomainEvent;
}
