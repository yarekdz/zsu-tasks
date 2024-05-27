namespace Tasks.Domain.Tasks
{
    public class TaskDateTimeStats
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ReleasedDate { get; set; }
    }
}
