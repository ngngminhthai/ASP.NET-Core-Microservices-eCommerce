namespace Contracts.Domains.Interfaces;

public interface IEntityAuditBase<T> : IEntityBase<T>, IAuditable
{
}