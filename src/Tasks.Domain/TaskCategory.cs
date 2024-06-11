namespace Tasks.Domain;

//public enum TaskCategory
//{
//    Default,
//    HighRisky
//}

public sealed record TaskCategory
{
    public string Name { get; }

    private TaskCategory(string name)
    {
        Name = name;
    }

    public static readonly TaskCategory Default = new(nameof(Default));
    public static readonly TaskCategory HighRisky = new(nameof(HighRisky));

    public override string ToString() => Name;

    public static TaskCategory FromName(string name)
    {
        return name switch
        {
            nameof(Default) => Default,
            nameof(HighRisky) => HighRisky,
            _ => throw new ArgumentException($"Invalid category name: {name}", nameof(name)),
        };
    }
}