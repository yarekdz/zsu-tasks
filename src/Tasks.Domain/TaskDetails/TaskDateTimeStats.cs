using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.TaskDetails
{
    public class TaskDateTimeStats
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ReleasedDate { get; set; }

        public CompletionRate? CompletionRate { get; set; }
    }
}
