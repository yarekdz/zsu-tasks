using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks
{
    public class TaskEstimation
    {
        //task breakdown
        //3 point estimate 

        public DateTime? StartDate { get; private set; }

        public DateTime DueDate { get; private set; }
        public DateTime DueTime { get; private set; }

        public Duration Duration { get; private set; }

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
