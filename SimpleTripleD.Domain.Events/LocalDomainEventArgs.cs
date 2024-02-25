using MediatR;

namespace SimpleTripleD.Domain.Events
{
    public class LocalDomainEventArgs : DomainEventArgs, INotification
    {
        public LocalDomainEventArgs(IntegrationEvent integrationEvent) : base(integrationEvent)
        {
        }
    }
}
