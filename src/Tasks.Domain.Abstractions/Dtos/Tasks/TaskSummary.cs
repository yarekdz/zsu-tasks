﻿namespace Tasks.Domain.Abstractions.Dtos.Tasks
{
    public record TaskSummary(
        Guid Id,
        Guid TaskId,
        string Title,
        string Description,
        string Category);
}
