using Dapr.Client;
using MediatR;

namespace SimpleTripleD.Domain.Events.Distributed
{
    internal class IntegrationEventHandler<TIntegrationEventArgs> : INotificationHandler<TIntegrationEventArgs>
        where TIntegrationEventArgs : IntegrationEventArgs
    {
        private readonly DaprClient _client;

        public IntegrationEventHandler(DaprClient client)
            => _client = client;

        public Task Handle(TIntegrationEventArgs args, CancellationToken cancellationToken)
            => _client.PublishEventAsync(args.PubSub, args.Topic, args, cancellationToken);
    }
}