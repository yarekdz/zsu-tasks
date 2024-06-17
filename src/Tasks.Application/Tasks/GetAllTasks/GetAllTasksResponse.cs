using Tasks.Domain;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetAllTasks
{
    public record GetAllTasksResponse(
        Guid Id, 
        TaskId TaskId, 
        string Title, 
        string? Description, 
        TaskCategory? Category);
}
