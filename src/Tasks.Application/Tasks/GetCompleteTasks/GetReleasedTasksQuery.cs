using Tasks.Application.Abstractions.Messaging;

namespace Tasks.Application.Tasks.GetCompleteTasks
{
    public record GetReleasedTasksQuery : IQuery<IEnumerable<GetReleasedTasksQueryResponse>>;
}
