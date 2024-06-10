using System.ComponentModel.DataAnnotations;
using Tasks.Domain.Events;

namespace Tasks.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public Guid Id { get; set; }
        [MaxLength(80)] public string CreatedBy { get; set; } = "System";
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

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
