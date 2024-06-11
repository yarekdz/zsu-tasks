using Tasks.Domain;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetTask;

public record GetTaskResponse(
    Guid Id, 
    TaskId TaskId, 
    string Title, 
    string Description, 
    TaskCategory Category);