using Dapr.Client;
using MediatR;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Cache
{
    internal class TryDeleteCacheEventHandler<TAggregateRoot, TKey> : INotificationHandler<TryDeleteCacheEventArgs<TKey>>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        private readonly DaprClient _daprClient;
        private readonly IMediator _mediator;

        public TryDeleteCacheEventHandler(DaprClient daprClient, IMediator mediator)
        {
            _mediator = mediator;
            _daprClient = daprClient;
        }

        public async Task Handle(TryDeleteCacheEventArgs<TKey> args, CancellationToken cancellationToken)
        {
            var aggregateRootEntry = await _daprClient.GetStateEntryAsync<TAggregateRoot>(args.StoreName,
                args.AggregateRootId!.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false);
            if (!await aggregateRootEntry.TryDeleteAsync(cancellationToken: cancellationToken).ConfigureAwait(false))
                await _mediator.Publish(aggregateRootEntry).ConfigureAwait(false);
        }
    }
}
