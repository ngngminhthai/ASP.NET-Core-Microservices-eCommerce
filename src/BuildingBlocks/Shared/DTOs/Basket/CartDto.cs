namespace Shared.DTOs.Basket
{
    public class CartDto
    {
        public long Id { get; set; }
        public string DocumentId { get; set; }
        public float TotalPrice { get; set; }
    }
}
