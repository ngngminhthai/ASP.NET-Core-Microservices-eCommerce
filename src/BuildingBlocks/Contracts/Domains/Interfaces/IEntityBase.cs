namespace Contracts.Domains.Interfaces;

public interface IEntityBase<T>
{
    T Id { get; set; }
}