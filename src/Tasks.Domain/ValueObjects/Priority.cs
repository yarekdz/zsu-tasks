using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.ValueObjects
{
    public record Priority
    {
        private const int MinPriority = 1;
        private const int MaxPriority = 5;

        private Priority(int value) => Value = value;
        public int Value { get; init; }

        public static Priority? Create(int value)
        {
            if (value is < MinPriority or > MaxPriority )
            {
                return null;
            }

            return new Priority(value);
        }
    }
}
