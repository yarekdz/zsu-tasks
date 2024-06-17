namespace Tasks.Domain.Abstractions.Dtos.Tasks
{
    public record TaskListDto(
        Guid Id,
        Guid TaskId,
        string Title,
        string Description,
        string Category,
        int Priority
    );
}
