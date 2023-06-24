namespace EventBus.IntegrationEvents
{
    public record BasketCheckout : IntegrationEvent
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string DocumentId { get; set; }
        public float TotalPrice { get; set; }
    }
}
