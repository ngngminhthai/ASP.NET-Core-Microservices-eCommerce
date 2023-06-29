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
        private readonly ISendEndpointProvider _sendEndpointProvider;


        public UpdateProductItemEventHandler(IMessageProducer messageProducer, IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _messageProducer = messageProducer;
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Handle(ProductItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Handle Product Updated Event");

            var productPriceChangedIntegrationEvent = new ProductPriceChangedIntegrationEvent { Name = notification.Name, Price = notification.Price };

            //send by implemented RabbitMq Producer
            _messageProducer.PublishEvent(productPriceChangedIntegrationEvent, "product-price-changes");

            //send by masstransit
            _publishEndpoint.Publish(new ProductEvent { Name = notification.Name, Price = notification.Price });

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:product-price-changes-masstransit"));

            //send by masstransit to specific queue
            await sendEndpoint.Send(new ProductEvent { Name = notification.Name, Price = notification.Price });
        }
    }
}
