namespace Tasks.Domain.Events.Person;

public sealed record PersonCreatedDomainEvent(Guid PersonId) : IDomainEvent;