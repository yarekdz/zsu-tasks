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
        DateTime? EstimatedStartDateTime,
        DateTime? EstimatedEndDateTime,
        //Duration? EstimatedWorkDuration,
        string Status);
}
