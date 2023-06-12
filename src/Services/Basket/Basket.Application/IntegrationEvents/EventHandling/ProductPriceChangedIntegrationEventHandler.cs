using Basket.Application.IntegrationEvents.Events;
using EventBus.Abstractions;

namespace Basket.Application.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {

        }
    }
}
