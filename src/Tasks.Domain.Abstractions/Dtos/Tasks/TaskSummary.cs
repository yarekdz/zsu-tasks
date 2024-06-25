using Tasks.Domain.States;

namespace Tasks.Domain.Abstractions.Dtos.Tasks
{
    public record TaskSummary(
        Guid Id,
        Guid TaskId,
        string MainInfo_Title,
        string MainInfo_Description,
        string MainInfo_Category,
        int MainInfo_Priority,
        Guid MainInfo_OwnerId,
        Guid MainInfo_AssigneeId,
        DateTime? Estimation_EstimatedStartDateTime,
        DateTime? Estimation_EstimatedEndDateTime,
        TodoTaskStatus Status);
}
