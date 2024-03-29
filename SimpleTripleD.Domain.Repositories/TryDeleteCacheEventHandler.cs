﻿using Dapr.Client;
using MediatR;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Repositories
{
    public class TryDeleteCacheEventHandler<TAggregateRoot, TKey> : INotificationHandler<TryDeleteCacheEventArgs<TKey>>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        protected readonly DaprClient _daprClient;

        public TryDeleteCacheEventHandler(DaprClient daprClient)
            => _daprClient = daprClient;

        public Task Handle(TryDeleteCacheEventArgs<TKey> notification, CancellationToken cancellationToken)
            => Task.Run(async () =>
            {
                var aggregateRootEntry = await _daprClient.GetStateEntryAsync<TAggregateRoot>(notification.StoreName, notification.Id!.ToString(), cancellationToken: cancellationToken);
                while (!await aggregateRootEntry.TryDeleteAsync(cancellationToken: cancellationToken));
            }, cancellationToken);
    }
}
