using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain;
using Tasks.Domain.Person;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.Create
{
    public record CreateTaskCommand(
        string Title, 
        string Description, 
        Category Category, 
        Priority Priority,
        PersonId OwnerId,
        PersonId AssigneeId) : ICommand<Guid>;
}
