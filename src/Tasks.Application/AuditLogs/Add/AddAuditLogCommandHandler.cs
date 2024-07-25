using Newtonsoft.Json;
using Tasks.Application.Abstractions.Messaging;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.AuditLog;
using Tasks.Domain.Shared;

namespace Tasks.Application.AuditLogs.Add
{
    internal class AddAuditLogCommandHandler : ICommandHandler<AddAuditLogCommand>
    {
        private readonly IAuditLogsCommandsRepository _auditLogsCommandsRepository;

        public AddAuditLogCommandHandler(IAuditLogsCommandsRepository auditLogsCommandsRepository)
        {
            _auditLogsCommandsRepository = auditLogsCommandsRepository;
        }

        public async Task<Result> Handle(AddAuditLogCommand command, CancellationToken cancellationToken)
        {
            var paramsArray = new List<object?>
            {
                //todo:v2 userDetails.Username,
                command.AdditionalDetails is string
                    ? command.AdditionalDetails.ToString()
                    : JsonConvert.SerializeObject(command.AdditionalDetails),
            };
            if (command.AdditionalParams != null) paramsArray.AddRange(command.AdditionalParams);

            var message = string.Format(AuditLogMessages.Messages[command.Action], paramsArray.ToArray());

            var log = new AuditLog
            {
                //todo: v2 
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Action = command.Action.ToString(),
                Message = message
            };

            await _auditLogsCommandsRepository.CreateAsync(log, cancellationToken);

            return Result.Success();
        }
    }
}
