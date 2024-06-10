using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Update
{
    public record UpdateTaskCommand(TaskId TaskId) : ICommand;
}
