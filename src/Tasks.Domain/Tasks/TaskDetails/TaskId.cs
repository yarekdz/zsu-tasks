namespace Tasks.Domain.Tasks.TaskDetails
{
    public record TaskId(Guid Value)
    {
        public override string ToString() => Value.ToString();
    };
}
