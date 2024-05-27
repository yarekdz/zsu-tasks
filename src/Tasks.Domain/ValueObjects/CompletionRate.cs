namespace Tasks.Domain.ValueObjects
{
    public class CompletionRate
    {
        private const double Tolerance = 0.0001;

        public double Rate { get; }

        public CompletionRate(double rate)
        {
            if (rate is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 100.");
            }

            Rate = rate;
        }

        public static CompletionRate Calculate(DateTime start, DateTime end, DateTime current)
        {
            if (current < start)
            {
                return new CompletionRate(0);
            }

            if (current >= end)
            {
                return new CompletionRate(100);
            }

            var totalDuration = end - start;
            var elapsedDuration = current - start;
            var rate = (elapsedDuration.TotalMilliseconds / totalDuration.TotalMilliseconds) * 100;

            return new CompletionRate(rate);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (CompletionRate)obj;
            return Math.Abs(Rate - other.Rate) < Tolerance;
        }

        public override int GetHashCode()
        {
            return Rate.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Rate}%";
        }
    }
}
