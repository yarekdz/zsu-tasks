namespace Tasks.Domain.AuditLog
{
    public class AuditLogMessages
    {
        public static Dictionary<AuditLogAction, string> Messages = new()
        {
            { AuditLogAction.LoginSuccess, "{0} login successfully." },
            { AuditLogAction.LoginFailed, "{0} tried to login." },
            { AuditLogAction.Logout, "{0} logout from system." },
            { AuditLogAction.TaskCreate, "{0} create task. Task title: {1}." },
            { AuditLogAction.TaskUpdate, "{0} update task. Task title: {1}." },
            { AuditLogAction.TaskDelete, "{0} delete task. Task title: {1}." },
            { AuditLogAction.TaskEstimate, "{0} estimate task. Task title: {1}." },
            { AuditLogAction.TaskStartWork, "{0} start work on task. Task title: {1}." },
        };
    }
}
