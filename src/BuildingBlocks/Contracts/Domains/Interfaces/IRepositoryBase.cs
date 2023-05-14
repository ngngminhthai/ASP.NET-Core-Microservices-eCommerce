using Microsoft.EntityFrameworkCore;

namespace Contracts.Domains.Interfaces
{
    public interface IRepositoryBase<T, K, TContext>
        where T : EntityBase<K>
        where TContext : DbContext
    {

    }
}
