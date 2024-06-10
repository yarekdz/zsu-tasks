using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Delete
{
    public record DeleteTaskCommand(TaskId TaskId) : ICommand;
}
