using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Person;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.Update
{
    public record UpdateTaskCommand(
        string Title,
        string Description,
        Priority Priority,
        PersonId AssigneeId) : ICommand
    {
        public TaskId? TaskId { get; set; }
    }
}