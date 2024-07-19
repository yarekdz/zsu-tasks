using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.Estimate
{
    public record EstimateCommandResponse(Duration? EstimatedWorkDuration);
}
