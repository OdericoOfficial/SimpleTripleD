using MediatR;

namespace SimpleTripleD.Domain.Events.Distributed
{
    public abstract class IntegrationEventArgs : DomainEventArgs, INotification
    {
        public string PubSub { get; set; }

        public string Topic { get; set; }

        public IntegrationEventArgs(string pubSub, string topic)
        {
            PubSub = pubSub;
            Topic = topic;
        }
    }
}