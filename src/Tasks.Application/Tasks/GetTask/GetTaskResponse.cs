using Tasks.Domain.Person;
using Tasks.Domain.States;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetTask;

public record GetTaskResponse(
    Guid Id, 
    TaskId TaskId,
    TaskMainInfo MainInfo,
    PersonId OwnerId,
    PersonId AssigneeId,
    TaskEstimation? Estimation,
    TodoTaskStatus Status,
    TaskStatistic? Statistic);