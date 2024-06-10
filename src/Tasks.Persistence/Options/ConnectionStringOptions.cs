using System.ComponentModel.DataAnnotations;

namespace Tasks.Persistence.Options;

internal sealed class ConnectionStringOptions
{
    public const string Position = "ConnectionStrings";

    [Required]
    public string Database { get; set; }
}