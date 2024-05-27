namespace Tasks.Domain.ValueObjects
{
    //The duration is the total time it takes for work to start and finish.
    public class Duration
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public Duration(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException("End time must be after start time");
            }

            Start = start;
            End = end;
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
    }
}
