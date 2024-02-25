using MediatR;

namespace SimpleTripleD.Domain.Events
{
    public abstract class DomainEventArgs : INotification
    {
        public IntegrationEvent IntegrationEvent { get; protected set; }

        public DomainEventArgs(IntegrationEvent integrationEvent)
            => IntegrationEvent = integrationEvent;
    }
}