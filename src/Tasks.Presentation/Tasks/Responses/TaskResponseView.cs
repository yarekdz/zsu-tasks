namespace Tasks.Presentation.Tasks.Responses
{
    public record TaskResponseView(
        Guid Id,
        Guid TaskId,
        string Title,
        string Description,
        string Category,
        int Priority,
        string Status);
}
