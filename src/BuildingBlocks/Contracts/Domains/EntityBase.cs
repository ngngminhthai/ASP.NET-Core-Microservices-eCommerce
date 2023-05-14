using Contracts.Domains.Interfaces;

namespace Contracts.Domains;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; }
}