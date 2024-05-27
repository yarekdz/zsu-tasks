namespace Tasks.Domain.Shared
{
    public class DomainValidationException : Exception
    {
        public string ErrorCode { get; set; }

        public DomainValidationException(Error error) : base(error.Message)
        {
            ErrorCode = error.Code;
        }
    }
}
