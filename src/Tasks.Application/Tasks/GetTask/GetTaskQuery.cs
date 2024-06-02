using Tasks.Application.Abstractions.Messaging;

namespace Tasks.Application.Tasks.GetTask
{
    public record GetTaskQuery(Guid TaskId) : IQuery<GetTaskResponse>;
}
