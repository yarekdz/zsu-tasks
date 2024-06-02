namespace Tasks.Domain.Abstractions.Repositories.Queries
{
    public interface IQueriesRepository<TDomainEntity>
    {
        Task<IEnumerable<TDomainEntity>> GetAll();
        Task<TDomainEntity?> Get(Guid id);
    }
}
