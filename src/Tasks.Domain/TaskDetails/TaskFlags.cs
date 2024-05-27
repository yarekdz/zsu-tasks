namespace Tasks.Domain.TaskDetails
{
    public class TaskFlags
    {
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public bool IsReleased { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
        public bool IsTerminated { get; set; }
    }
}
