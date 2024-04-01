namespace SimpleTripleD.Domain.Events.Distributed
{
    public interface IIntegrationEvents
    {
        IEnumerable<IntegrationEventArgs> IntegrationEvents { get; }
        void AddIntegrationEvent<TIntegrationEventArgs>(TIntegrationEventArgs args) where TIntegrationEventArgs : IntegrationEventArgs;
        void ClearIntegrationEvents();
    }
}