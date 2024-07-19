namespace Tasks.Presentation.Tasks.Responses
{
    public record TaskDetailsResponseView(
        Guid Id,
        Guid TaskId,
        string Title,
        string Description,
        string Category,
        int Priority,
        Guid OwnerId,
        Guid AssigneeId,
        TaskDetailsEstimationResponseView Estimation,
        string Status,
        TaskDetailsStatsResponseView Stats);

    public record TaskDetailsEstimationResponseView(
        string StartDateTime,
        string EndDateTime,
        string? EstimatedWorkDuration);

    public record TaskDetailsStatsResponseView(
        string CreatedDate,
        string? StartedDate,
        string? CompletionDate,
        string? VerifiedDate,
        string? ApprovedDate,
        string? ReleasedDate);
}
