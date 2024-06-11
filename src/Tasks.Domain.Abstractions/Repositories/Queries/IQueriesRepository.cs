namespace Tasks.Domain.Abstractions.Repositories.Queries
{
    public interface IQueriesRepository<TDomainEntity>
    {
        Task<TDomainEntity?> GetAsync(Guid id, CancellationToken ct);
    }
}
