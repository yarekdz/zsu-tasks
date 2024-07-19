using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.Estimate
{
    public record EstimateCommand(
        DateTime EstimatedStartDateTime,
        DateTime EstimatedEndDateTime) : ICommand<EstimateCommandResponse>
    {
        public TaskId? TaskId { get; set; }
    }
}
