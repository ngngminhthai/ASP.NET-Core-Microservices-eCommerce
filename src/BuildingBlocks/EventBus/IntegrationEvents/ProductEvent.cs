namespace EventBus.IntegrationEvents
{
    public record ProductEvent : IntegrationEvent
    {
        public int Id { get; set; }
    }
}
