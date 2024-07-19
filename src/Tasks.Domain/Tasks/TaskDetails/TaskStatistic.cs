using Tasks.Domain.Abstractions;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks.TaskDetails
{
    public class TaskStatistic : Entity
    {
        public TaskStatistic(){}
        public TaskStatistic(TaskId taskId)
        {
            TaskId = taskId;
            CreatedAt = DateTime.UtcNow;
        }

        public TaskId TaskId { get; }
        public TodoTask? Task { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ReleasedDate { get; set; }

        public Duration? ActualWorkDuration { get; set; }

        //todo: new method to calculate completion rate from Actual StartedDate to Estimated EndDate 
        // based on DateTime.Now

        //todo: new method to compare Estimated and Actual Duration
        //public CompletionRate? CompletionRate => CompletionRate.Calculate(ActualWorkDuration.Start,
        //            ActualWorkDuration.End, DateTime.UtcNow);

    }
}

