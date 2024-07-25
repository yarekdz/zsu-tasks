using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.AuditLog;

namespace Tasks.Application.AuditLogs.Add
{
    public record AddAuditLogCommand(
        AuditLogAction Action,
        //todo:v2
        //string User, 
        object? AdditionalDetails = null,
        string[]? AdditionalParams = null) : ICommand;
}
