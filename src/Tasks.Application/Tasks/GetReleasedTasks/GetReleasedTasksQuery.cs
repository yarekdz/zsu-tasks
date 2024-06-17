using Tasks.Application.Abstractions.Messaging;

namespace Tasks.Application.Tasks.GetReleasedTasks
{
    public record GetReleasedTasksQuery : IQuery<IEnumerable<GetReleasedTasksQueryResponse>>;
}
