using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.TaskDetails
{
    public class TaskEstimation
    {
        public DateTime EstimatedStartDateTime { get; set; }

        public DateTime EstimatedEndDateTime { get; set; }

        public Duration? EstimatedWorkDuration { get; set; }

        #region v2 3 point estimate

        //todo v2: 3 point estimate 
        //public double Optimistic { get; set; }
        //public double Pessimistic { get; set; }
        //public double MostLikely { get; set; }

        //public double GetThreePointEstimate()
        //{
        //    return (Optimistic + 4 * MostLikely + Pessimistic) / 6;
        //}

        #endregion

    }
}
