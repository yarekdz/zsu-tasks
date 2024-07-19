using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.StartWork
{
    public class StartWorkCommand : ICommand
    {
        public TaskId? TaskId { get; set; }
    }
}
