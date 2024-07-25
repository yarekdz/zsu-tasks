using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Person;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.Update
{
    public record UpdateTaskCommand : ICommand
    {
        public TaskId? TaskId { get; set; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public Priority? Priority { get; init; }
        public PersonId? AssigneeId { get; init; }
    }
}