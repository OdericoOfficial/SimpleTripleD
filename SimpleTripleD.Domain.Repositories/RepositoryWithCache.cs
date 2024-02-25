using Dapr.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Repositories
{
    public class RepositoryWithCache<TDbContext, TAggregateRoot, TKey> : Repository<TDbContext, TAggregateRoot, TKey>, IRepositoryWithCache<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        protected readonly IMediator _mediator;
        protected readonly DaprClient _daprClient;

        public RepositoryWithCache(IUnitOfWork unitOfWork, TDbContext context,
            IMediator mediator, DaprClient daprClient) : base(unitOfWork, context)
        {
            _daprClient = daprClient;
            _mediator = mediator;
        }

        public async Task<TAggregateRoot> FirstAsync(string storeName, TKey id, CancellationToken cancellationToken = default)
        {
            string storeKey = id!.ToString()!;
            var aggregateRoot = await _daprClient.GetStateAsync<TAggregateRoot>(storeName, storeKey, cancellationToken: cancellationToken);
            if (aggregateRoot is not null)
                return aggregateRoot;
            aggregateRoot = await this.FirstAsync(id, cancellationToken);
            await _daprClient.SaveStateAsync(storeName, storeKey, aggregateRoot, cancellationToken: cancellationToken);
            return aggregateRoot;
        }

        public async Task<TAggregateRoot?> FirstOrDefaultAsync(string storeName, TKey id, CancellationToken cancellationToken = default)
        {
            string storeKey = id!.ToString()!;
            var aggregateRoot = await _daprClient.GetStateAsync<TAggregateRoot>(storeName, storeKey, cancellationToken: cancellationToken);
            if (aggregateRoot is not null)
                return aggregateRoot;
            aggregateRoot = await this.FirstOrDefaultAsync(id, cancellationToken);
            if (aggregateRoot is not null)
                await _daprClient.SaveStateAsync(storeName, storeKey, aggregateRoot, cancellationToken: cancellationToken);
            return aggregateRoot;
        }
 
        public async Task<TAggregateRoot> InsertAsync(string storeName, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            aggregateRoot = await InsertAsync(aggregateRoot, true, cancellationToken);
            await _mediator.Publish(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, storeName), cancellationToken);
            return aggregateRoot;
        }

        public async Task<TAggregateRoot> UpdateAsync(string storeName, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            aggregateRoot = await UpdateAsync(aggregateRoot, true, cancellationToken);
            await _mediator.Publish(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, storeName), cancellationToken);
            return aggregateRoot;
        }

        public async Task DeleteAsync(string storeName, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(aggregateRoot, true, cancellationToken);
            await _mediator.Publish(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, storeName), cancellationToken);
        }

        public async Task DeleteAsync(string storeName, TKey id, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(id, true, cancellationToken);
            await _mediator.Publish(new TryDeleteCacheEventArgs<TKey>(id, storeName), cancellationToken);
        }
    }
}