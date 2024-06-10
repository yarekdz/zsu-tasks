namespace Tasks.Domain.Abstractions.Dtos
{
    public class UpdateResponse<TDomainEntity>
    {
        public TDomainEntity? OldValue { get; set; }
        public TDomainEntity NewValue { get; set; }
    }
}
