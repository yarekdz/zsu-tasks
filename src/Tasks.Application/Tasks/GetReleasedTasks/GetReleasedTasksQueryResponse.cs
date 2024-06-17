using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.GetReleasedTasks
{
    public record GetReleasedTasksQueryResponse(
        Guid Id,
        TaskId TaskId,
        string Title,
        string? Description,
        TaskCategory? Category,
        Priority? Priority);
}
