using Tasks.Domain.Events;

namespace Tasks.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity()
        {

        }

        public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
