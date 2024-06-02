namespace Tasks.Domain.Abstractions.Repositories.Commands;

public interface ICommandsRepository<in TDomainEntity>
{
    Task CreateAsync(TDomainEntity domainModel);
    //Task<UpdateResponse<TDomainEntity>> UpdateAsync(TDomainEntity domainModel);
    Task DeleteAsync(string id, bool ignoreDependencies = false);
    Task DeleteAsync(string[] ids, bool ignoreDependencies = false);
}