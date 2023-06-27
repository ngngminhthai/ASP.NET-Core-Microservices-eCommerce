namespace EventBus.IntegrationEvents
{
    public record ProductEvent : IntegrationEvent
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
