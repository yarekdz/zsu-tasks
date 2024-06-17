using Tasks.Domain.States;

namespace Tasks.Domain.Abstractions.Dtos.Tasks
{
    public record TaskSummary(
        Guid Id,
        Guid TaskId,
        string Title,
        string Description,
        string Category,
        int Priority,
        Guid OwnerId,
        Guid AssigneeId,
        DateTime? EstimatedStartDateTime,
        DateTime? EstimatedEndDateTime,
        TodoTaskStatus Status);
}
