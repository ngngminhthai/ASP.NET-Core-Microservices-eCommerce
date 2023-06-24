using Contracts.Domains;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public class Cart : EntityBase<long>
    {
        public string UserName { get; set; }
        public List<CartItem>? BasketItems { get; set; } = new();

        public void AddItem(CartItem item)
        {
            BasketItems.Add(item);
        }

    }
}
