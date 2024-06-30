namespace Tasks.Domain.Shared
{
    public class DomainValidationException : Exception
    {
        public Error Error { get; set; }

        public DomainValidationException(Error error) : base(error.Message)
        {
            Error = error;
        }
    }
}
