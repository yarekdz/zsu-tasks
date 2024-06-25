using Tasks.Domain;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.GetAllTasks
{
    public record GetAllTasksResponse(
        Guid Id, 
        TaskId TaskId, 
        string Title, 
        string? Description, 
        TaskCategory? Category,
        Priority? Priority,
        TodoTaskStatus Status);
}
