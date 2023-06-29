using MediatR;
using Product.Domain.AggregateModels.ProductAggregate;
using Shared.SeedWork;

namespace Product.Application.Features.ProductItems.Commands.UpdateProductItem
{
    public class UpdateProductItemCommandHandler : IRequestHandler<UpdateProductItemCommand, ApiResult<long>>
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly IMediator _mediator;


        public UpdateProductItemCommandHandler(IProductItemRepository productItemRepository, IMediator mediator)
        {
            _productItemRepository = productItemRepository;
            _mediator = mediator;
        }

        public async Task<ApiResult<long>> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
        {
            request.ProductItem.UpdatedProductItem();
            var updatedProductItem = await _productItemRepository.UpdateOrderAsync(request.ProductItem);


            //Publish event
            foreach (var domainEvent in request.ProductItem.DomainEvents())
            {
                await _mediator.Publish(domainEvent);
            }

            return new ApiResult<long>(true);
        }
    }
}
