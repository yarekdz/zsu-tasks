namespace Tasks.Domain.ValueObjects
{
    public record Priority
    {
        private const int MinPriority = 1;
        private const int MaxPriority = 5;

        private Priority(int value) => Value = value;
        public int Value { get; init; }

        public static Priority Create(int? value = null)
        {
            if (value is null or < MinPriority or > MaxPriority )
            {
                return new Priority(MinPriority);
            }

            return new Priority((int)value);
        }
    }
}
