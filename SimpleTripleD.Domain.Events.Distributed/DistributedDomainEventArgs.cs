using MediatR;

namespace SimpleTripleD.Domain.Events.Distributed
{
    public class DistributedDomainEventArgs : DomainEventArgs, INotification
    {
        public string PubSub { get; set; }

        public string Topic { get; set; }

        public DistributedDomainEventArgs(IntegrationEvent integrationEvent, string pubSub, string topic) : base(integrationEvent)
        {
            PubSub = pubSub;
            Topic = topic;
        }
    }
}