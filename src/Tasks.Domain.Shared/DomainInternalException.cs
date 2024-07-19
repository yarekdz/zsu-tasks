namespace Tasks.Domain.Shared;

public class DomainInternalException : Exception
{
    public Error Error { get; set; }

    public DomainInternalException(Error error) : base(error.Message)
    {
        Error = error;
    }
}