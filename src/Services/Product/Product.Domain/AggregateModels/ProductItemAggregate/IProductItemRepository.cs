using Contracts.Domains.Interfaces;

namespace Product.Domain.AggregateModels.ProductAggregate
{
    public interface IProductItemRepository : IRepositoryBase<ProductItem, long>
    {
        Task<ProductItem> UpdateOrderAsync(ProductItem productItem);

    }
}
