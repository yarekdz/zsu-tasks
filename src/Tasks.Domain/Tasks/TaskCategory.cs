namespace Tasks.Domain.Tasks;

public sealed record TaskCategory
{
    private readonly string _value;

    private TaskCategory(string value)
    {
        _value = value;
    }

    public static readonly TaskCategory Default = new(nameof(Default));
    public static readonly TaskCategory HighRisky = new(nameof(HighRisky));

    public override string ToString() => _value;

    public static TaskCategory Create(string value)
    {
        return value switch
        {
            nameof(Default) => Default,
            nameof(HighRisky) => HighRisky,
            _ => throw new ArgumentException($"Invalid category value: {value}", nameof(value)),
        };
    }
}