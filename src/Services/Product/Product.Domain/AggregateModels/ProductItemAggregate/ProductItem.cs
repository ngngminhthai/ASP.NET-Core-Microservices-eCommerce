using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Product.Domain.Events.ProductItemAggregate;

namespace Product.Domain.AggregateModels.ProductAggregate
{
    public class ProductItem : AuditableEventEntity<long>, IEventEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }

        public ProductItem UpdatedProductItem()
        {
            AddDomainEvent(new ProductItemUpdatedEvent { Name = this.Name, Price = this.Price });
            return this;
        }


    }
}
