namespace SimpleTripleD.Domain.Events
{
    public abstract class IntegrationEvent
    {
        public Guid EventId { get; protected set; }

        public string EventType { get; protected set; }

        public DateTime Timestamp { get; }

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            EventType = GetType().Name;
            Timestamp = DateTime.UtcNow;
        }
    }
}
