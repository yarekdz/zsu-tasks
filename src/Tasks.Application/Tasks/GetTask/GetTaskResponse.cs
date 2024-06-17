using Tasks.Domain;
using Tasks.Domain.Person;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.GetTask;

public record GetTaskResponse(
    Guid Id, 
    TaskId TaskId, 
    string Title, 
    string Description, 
    TaskCategory Category,
    Priority Priority,
    PersonId OwnerId,
    PersonId AssigneeId,
    DateTime? EstimatedStartDateTime,
    DateTime? EstimatedEndDateTime,
    Duration? EstimatedWorkDuration,
    TodoTaskStatus Status);