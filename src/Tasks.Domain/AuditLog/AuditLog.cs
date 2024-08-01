using Tasks.Domain.Abstractions;

namespace Tasks.Domain.AuditLog
{
    public class AuditLog : Entity
    {
        //TODO: V2
        //public Guid? CompanyId { get; set; }
        //public string Type { get; set; }
        //public string UserId { get; set; }

        public string Action { get; set; }
        public string Message { get; set; }
        public Guid ObjectRelateId { get; set; }
    }
}
