namespace Tasks.Domain.Person;

public record PersonId(Guid Value)
{
    public override string ToString() => Value.ToString();
}