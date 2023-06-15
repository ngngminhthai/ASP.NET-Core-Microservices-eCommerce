using EventBus.IntegrationEvents;
using MassTransit;

namespace Basket.Application.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventMassTransitHandler : IConsumer<ProductEvent>
    {
        public Task Consume(ConsumeContext<ProductEvent> context)
        {
            Console.WriteLine("Consume product price changes succesfully");
            return Task.CompletedTask;
        }
    }
}
