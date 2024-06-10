namespace Tasks.Domain.Shared
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", ErrorType.Failure);

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error NotFound(string code, string message) =>
            new(code, message, ErrorType.NotFound);

        public static Error Validation(string code, string message) =>
            new(code, message, ErrorType.Validation);

        public static Error Failure(string code, string message) =>
            new(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message) =>
            new(code, message, ErrorType.Conflict);
    }

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3
    }
}
