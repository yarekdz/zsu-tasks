using Tasks.Domain.ValueObjects;

namespace Tasks.Domain;

public class TaskSchedule
{
    public IReadOnlyList<Dependency> Dependencies { get; private set; }

    public Priority Priority { get; private set; }
}