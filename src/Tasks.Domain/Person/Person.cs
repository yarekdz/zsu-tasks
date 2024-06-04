using Tasks.Domain.Abstractions;
using Tasks.Domain.Events.Person;
using Tasks.Domain.Shared;

namespace Tasks.Domain.Person
{
    public class Person : Entity
    {
        protected Person(PersonId id, string email)
        {
            Id = id;
            Email = email;
        }

        public PersonId Id { get; set; }
        public string Email { get; set; } //todo: regexp

        public static Person Create(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainValidationException(Errors.PersonErrors.Create.InvalidEmail);
            }

            var newPerson = new Person(new PersonId(Guid.NewGuid()), email);

            newPerson.Raise(new PersonCreatedDomainEvent(newPerson.Id.Value));
            return newPerson;
        }

    }
}
