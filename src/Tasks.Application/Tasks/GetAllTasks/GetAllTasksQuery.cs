using Tasks.Application.Abstractions.Messaging;

namespace Tasks.Application.Tasks.GetAllTasks
{
    public record GetAllTasksQuery : IQuery<GetAllTasksResponse[]>;
}
