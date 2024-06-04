using Tasks.Domain.Errors;
using Tasks.Domain.Shared;

namespace Tasks.Domain.ValueObjects
{
    //The duration is the total time it takes for work to start and finish.
    public class Duration
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public Duration(DateTime? start, DateTime? end)
        {
            if (!start.HasValue || !end.HasValue)
            {
                throw new DomainValidationException(TaskErrors.Estimate.InvalidDuration);
            }
            if (start > end)
            {
                throw new DomainValidationException(TaskErrors.Estimate.StartDateCouldNotBeGreaterThanEndDate);
            }

            Start = start.Value;
            End = end.Value;
        }

        public TimeSpan TotalDuration => End - Start;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Duration)obj;
            return Start == other.Start && End == other.End;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public override string ToString()
        {
            int days = TotalDuration.Days;
            int hours = TotalDuration.Hours;
            int minutes = TotalDuration.Minutes;
            int seconds = TotalDuration.Seconds;


            string formattedString = "Duration: ";

            if (days > 0)
            {
                formattedString += $"{days}d ";
            }
            if (hours > 0)
            {
                formattedString += $"{hours}h ";
            }
            if (minutes > 0)
            {
                formattedString += $"{minutes}m";
            }

            if (seconds > 0 || formattedString == "")
            {
                formattedString += $"{seconds}s";
            }

            return formattedString.Trim();
        }
    }
}
