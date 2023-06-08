using MediatR;
using Product.Domain.Events.ProductItemAggregate;

namespace Product.Application.Features.ProductItems
{
    public class ProductItemDomainHandler : INotificationHandler<ProductItemUpdatedEvent>
    {
        public Task Handle(ProductItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle Product Updated Event");


            return Task.CompletedTask;
        }
    }
}
