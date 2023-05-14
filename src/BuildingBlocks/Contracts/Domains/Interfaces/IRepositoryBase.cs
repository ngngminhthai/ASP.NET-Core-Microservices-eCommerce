namespace Contracts.Domains.Interfaces
{
    public interface IRepositoryBase<T, K> : IRepositoryQueryBase<T, K>
        where T : EntityBase<K>
    {
    }
}
