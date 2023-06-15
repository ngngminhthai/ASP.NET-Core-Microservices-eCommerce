using Basket.Application.IntegrationEvents.Events;
using EventBus.Abstractions;

namespace Basket.Application.IntegrationEvents.EventHandling
{
    public class ProductDeleteIntegrationEventHandler : IIntegrationEventHandler<ProductDeleteIntegrationEvent>
    {
        public async Task Handle(ProductDeleteIntegrationEvent @event)
        {

        }
    }
}
