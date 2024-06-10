using Tasks.Domain.Abstractions.Dtos;

namespace Tasks.Domain.Abstractions.Repositories.Commands;

public interface ICommandsRepository<TDomainEntity>
    where TDomainEntity : Entity
{
    Task CreateAsync(TDomainEntity domainModel, CancellationToken ct);
    Task<UpdateResponse<TDomainEntity>> UpdateAsync(TDomainEntity domainModel, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct, bool ignoreDependencies = false);
    Task DeleteAsync(Guid[] ids, CancellationToken ct, bool ignoreDependencies = false);
}