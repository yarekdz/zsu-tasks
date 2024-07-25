namespace Tasks.Presentation.Tasks.Requests
{
    public record UpdateTaskRequest
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public int? Priority { get; init; }
        public Guid? AssigneeId { get; init; }
    }
}
