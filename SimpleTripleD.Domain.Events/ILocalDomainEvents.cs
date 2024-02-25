namespace SimpleTripleD.Domain.Events
{
    public interface ILocalDomainEvents
    {
        IEnumerable<LocalDomainEventArgs> LocalEvents { get; }
        void AddLocalEvent(IntegrationEvent integrationEvent);
        void ClearLocalEvents();
    }
}