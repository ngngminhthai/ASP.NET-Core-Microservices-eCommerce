using Contracts.Messages;
using EventBus.IntegrationEvents;
using MassTransit;
using MediatR;
using Product.Application.IntegrationEvents;
using Product.Domain.Events.ProductItemAggregate;

namespace Product.Application.Features.ProductItems
{
    public class UpdateProductItemEventHandler : INotificationHandler<ProductItemUpdatedEvent>
    {
        private readonly IMessageProducer _messageProducer;
        private readonly IPublishEndpoint _publishEndpoint;


        public UpdateProductItemEventHandler(IMessageProducer messageProducer, IPublishEndpoint publishEndpoint)
        {
            _messageProducer = messageProducer;
            _publishEndpoint = publishEndpoint;
        }

        public Task Handle(ProductItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle Product Updated Event");

            var productPriceChangedIntegrationEvent = new ProductPriceChangedIntegrationEvent { Name = notification.Name, Price = notification.Price };

            _messageProducer.PublishEvent(productPriceChangedIntegrationEvent, "product-price-changes");
            _publishEndpoint.Publish(productPriceChangedIntegrationEvent);
            _publishEndpoint.Publish(new ProductEvent { Id = 1 });
            return Task.CompletedTask;
        }
    }
}
