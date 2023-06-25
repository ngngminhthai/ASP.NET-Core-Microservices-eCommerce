namespace Shared.DTOs.Basket
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}