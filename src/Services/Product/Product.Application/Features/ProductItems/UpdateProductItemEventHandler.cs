using Contracts.Messages;
using MediatR;
using Product.Application.IntegrationEvents;
using Product.Domain.Events.ProductItemAggregate;

namespace Product.Application.Features.ProductItems
{
    public class UpdateProductItemEventHandler : INotificationHandler<ProductItemUpdatedEvent>
    {
        private readonly IMessageProducer _messageProducer;

        public UpdateProductItemEventHandler(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        public Task Handle(ProductItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle Product Updated Event");

            var productPriceChangedIntegrationEvent = new ProductPriceChangedIntegrationEvent { Name = notification.Name, Price = notification.Price };

            _messageProducer.PublishEvent(productPriceChangedIntegrationEvent);
            return Task.CompletedTask;
        }
    }
}
