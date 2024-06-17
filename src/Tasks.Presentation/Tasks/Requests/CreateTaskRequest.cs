namespace Tasks.Presentation.Tasks.Requests
{
    public record CreateTaskRequest(
        string Title,
        string Description,
        string Category,
        int Priority,
        Guid OwnerId,
        Guid AssigneeId);
}