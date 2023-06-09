using Infrastructure.Common.Repositories;
using Product.Domain.AggregateModels.ProductAggregate;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure.Repositories
{
    public class ProductItemRepository : RepositoryBase<ProductItem, long, ProductDbContext>, IProductItemRepository
    {
        public ProductItemRepository(ProductDbContext context) : base(context)
        {
        }

        public async Task<ProductItem> UpdateOrderAsync(ProductItem productItem)
        {
            await UpdateAsync(productItem);
            return productItem;
        }
    }
}
