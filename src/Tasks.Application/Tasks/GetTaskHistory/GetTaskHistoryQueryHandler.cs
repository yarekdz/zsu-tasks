using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Shared;

namespace Tasks.Application.Tasks.GetTaskHistory;

internal sealed class GetTaskHistoryQueryHandler : IQueryHandler<GetTaskHistoryQuery, IEnumerable<GetTaskHistoryResponse>>
{
    private readonly ITaskQueriesRepository _taskQueriesRepository;

    public GetTaskHistoryQueryHandler(ITaskQueriesRepository taskQueriesRepository)
    {
        _taskQueriesRepository = taskQueriesRepository;
    }

    public async Task<Result<IEnumerable<GetTaskHistoryResponse>>> Handle(
        GetTaskHistoryQuery request, 
        CancellationToken cancellationToken)
    {
        var taskAuditLogs = await _taskQueriesRepository.GetAuditLogAsync(request.TaskId.Value, cancellationToken);

        return Result.Success(taskAuditLogs.Select(al =>
            new GetTaskHistoryResponse(
                al.Action, 
                al.Message, 
                al.CreatedAt)));
    }
}