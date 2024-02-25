using Dapr.Client;
using MediatR;

namespace SimpleTripleD.Domain.Events.Distributed
{
    public class DistributedEventHandler : INotificationHandler<DistributedDomainEventArgs>
    {
        private readonly DaprClient _client;

        public DistributedEventHandler(DaprClient client)
            => _client = client;

        public Task Handle(DistributedDomainEventArgs notification, CancellationToken cancellationToken)
            => _client.PublishEventAsync(notification.PubSub, notification.Topic, notification.IntegrationEvent, cancellationToken);
    }
}