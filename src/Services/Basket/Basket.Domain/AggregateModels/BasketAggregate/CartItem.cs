using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public class CartItem
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public long BasketId { get; set; }

        [ForeignKey(nameof(BasketId))]
        public Cart? Basket { get; set; }
    }
}
