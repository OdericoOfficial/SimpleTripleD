namespace SimpleTripleD.Domain.Events.Distributed
{
    public interface IDistributedDomainEvents
    {
        IEnumerable<DistributedDomainEventArgs> DistributedEvents { get; }
        void AddDistributedEvent(IntegrationEvent integrationEvent, string pubSub, string topic);
        void ClearDistributedEvents();
    }
}