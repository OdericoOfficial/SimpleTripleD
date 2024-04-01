using MediatR;

namespace SimpleTripleD.Domain.Events
{
    public abstract class DomainEventArgs : INotification
    {
        public Guid EventId { get; protected set; }

        public string EventType { get; protected set; }

        public DateTime Timestamp { get; }

        public DomainEventArgs()
        {
            EventId = Guid.NewGuid();
            EventType = GetType().Name;
            Timestamp = DateTime.UtcNow;
        }
    }
}