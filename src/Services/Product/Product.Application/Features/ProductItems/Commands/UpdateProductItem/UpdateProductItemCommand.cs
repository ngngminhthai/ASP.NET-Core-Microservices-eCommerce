using MediatR;
using Product.Domain.AggregateModels.ProductAggregate;
using Shared.SeedWork;

namespace Product.Application.Features.ProductItems.Commands.UpdateProductItem
{
    public class UpdateProductItemCommand : IRequest<ApiResult<long>>
    {
        public ProductItem ProductItem { get; set; }

        public UpdateProductItemCommand(ProductItem productItem)
        {
            ProductItem = productItem;
        }
    }
}
