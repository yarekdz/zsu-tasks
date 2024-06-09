namespace Tasks.Persistence.Options;

internal sealed class ConnectionStringOptions
{
    public const string Position = "ConnectionStrings";
    public string Database { get; set; }
}