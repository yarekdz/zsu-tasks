namespace Tasks.Presentation.Tasks.Requests
{
    public record UpdateTaskRequest(
        string Title,
        string Description,
        int Priority,
        Guid AssigneeId);
}
