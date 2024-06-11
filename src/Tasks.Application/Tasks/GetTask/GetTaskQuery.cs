using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetTask
{
    public record GetTaskQuery(TaskId TaskId) : IQuery<GetTaskResponse>;
}
