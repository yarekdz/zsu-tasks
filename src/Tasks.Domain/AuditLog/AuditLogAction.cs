using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.AuditLog
{
    public enum AuditLogAction
    {
        [Display(Name = "Login success")]
        LoginSuccess = 1,
        [Display(Name = "Login failed")]
        LoginFailed = 2,
        [Display(Name = "Logout")]
        Logout = 3,
        [Display(Name = "Task create")]
        TaskCreate = 4,
        [Display(Name = "Task update")]
        TaskUpdate = 5,
        [Display(Name = "Task delete")]
        TaskDelete = 6,
        [Display(Name = "Task estimate")]
        TaskEstimate = 7,
        [Display(Name = "Task start work")]
        TaskStartWork = 8
    }
}
