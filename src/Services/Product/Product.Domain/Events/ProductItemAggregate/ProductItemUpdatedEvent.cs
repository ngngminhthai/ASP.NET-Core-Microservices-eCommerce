using Contracts.Common.Events;

namespace Product.Domain.Events.ProductItemAggregate
{
    public class ProductItemUpdatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
