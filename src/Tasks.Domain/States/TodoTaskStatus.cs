namespace Tasks.Domain.States
{
    public enum TodoTaskStatus
    {
        ConceptInactive = 0,
        Created = 1,
        Assigned = 2,
        Estimated = 3,
        Scheduled = 4,
        WorkStarted = 5,
        WorkCompleted = 6,
        Verified = 7,
        Approved = 8,
        Released = 9,
        WorkTerminated = 10
    }
}
