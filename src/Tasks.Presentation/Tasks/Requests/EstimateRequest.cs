namespace Tasks.Presentation.Tasks.Requests
{
    public record EstimateRequest(
        DateTime EstimatedStartDateTime,
        DateTime EstimatedEndDateTime);
}
