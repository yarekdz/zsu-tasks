using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Tasks.TaskDetails;

namespace Tasks.Application.Tasks.GetTaskHistory
{
    public record GetTaskHistoryQuery(TaskId TaskId) : IQuery<IEnumerable<GetTaskHistoryResponse>>;
}
