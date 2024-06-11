using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain;

namespace Tasks.Application.Tasks.GetCompleteTasks
{
    public record GetReleasedTasksQueryResponse(
        Guid Id,
        TaskId TaskId,
        string Title,
        string? Description,
        TaskCategory? Category);
}
