using Tasks.Domain.Abstractions;
using Tasks.Domain.Events.Person;
using Tasks.Domain.Shared;

namespace Tasks.Domain.Person
{
    public class Person : Entity
    {
        protected Person(Guid id, string email)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;

            PersonId = new PersonId(id);
            Email = email;
        }

        public PersonId PersonId { get; set; }
        public string Email { get; set; } //todo: regexp

        public static Person Create(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainValidationException(Errors.PersonErrors.Create.InvalidEmail);
            }

            var newPerson = new Person(Guid.NewGuid(), email);

            newPerson.Raise(new PersonCreatedDomainEvent(newPerson.PersonId.Value));
            return newPerson;
        }

    }
}
