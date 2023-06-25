namespace Shared.DTOs.Basket
{
    public class CartDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public float TotalPrice { get; set; }
        public List<CartItemDto> Items { get; set; } = new();

    }
}
