namespace Tasks.Domain.Abstractions.Repositories.Queries
{
    public interface IQueriesRepository<TDomainEntity>
    {
        Task<IEnumerable<TDomainEntity>> GetAll(CancellationToken ct);
        Task<TDomainEntity?> Get(Guid id, CancellationToken ct);
    }
}
