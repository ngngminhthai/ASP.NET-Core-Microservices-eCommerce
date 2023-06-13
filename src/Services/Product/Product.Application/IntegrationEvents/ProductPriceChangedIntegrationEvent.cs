using EventBus.IntegrationEvents;

namespace Product.Application.IntegrationEvents
{
    public record ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
