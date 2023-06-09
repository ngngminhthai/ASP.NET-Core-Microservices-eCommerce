using Contracts.Messages;
using MediatR;
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

            _messageProducer.SendMessage("Send to RabbitMQ");
            return Task.CompletedTask;
        }
    }
}
