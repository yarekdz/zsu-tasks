using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.TaskDetails
{
    public class TaskEstimation
    {
        //task breakdown
        //3 point estimate 

        public DateTime StartDateTime { get; set; }

        public DateTime DueDateTime { get; set; }

        public Duration? WorkDuration { get; set; }

        //3 points estimation
        public double Optimistic { get; set; }
        public double Pessimistic { get; set; }
        public double MostLikely { get; set; }

        public double GetThreePointEstimate()
        {
            return (Optimistic + 4 * MostLikely + Pessimistic) / 6;
        }
    }
}
