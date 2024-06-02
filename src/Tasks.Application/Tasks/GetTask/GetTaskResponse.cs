namespace Tasks.Application.Tasks.GetTask;

public record GetTaskResponse(Guid Id, string Title, string Description, string Category);