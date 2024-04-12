using Dapr.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleTripleD.Domain.Cache;
using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Infrastruct.Dapr;

namespace SimpleTripleD.Infrastruct.Repositories
{
    internal class RepositoryWithCache<TDbContext, TAggregateRoot, TKey> : Repository<TDbContext, TAggregateRoot, TKey>, IRepositoryWithCache<TAggregateRoot, TKey>, IReadOnlyRepositoryWithCache<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        protected readonly IMediator _mediator;
        protected readonly DaprOptions _daprOptions;
        protected readonly DaprClient _daprClient;

        public RepositoryWithCache(IUnitOfWork<TDbContext> unitOfWork, IMediator mediator,
            IOptions<DaprOptions> options, DaprClient daprClient) : base(unitOfWork)
        {
            _daprOptions = options.Value;
            _daprClient = daprClient;
            _mediator = mediator;
        }

        public async Task<TAggregateRoot> FirstWithCacheAsync(TKey id, CancellationToken cancellationToken = default)
        {
            string storeKey = id!.ToString()!;
            var aggregateRoot = await _daprClient.GetStateAsync<TAggregateRoot>(_daprOptions.StateStore, storeKey, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (aggregateRoot is not null)
                return aggregateRoot;
            aggregateRoot = await this.FirstAsync(id, cancellationToken).ConfigureAwait(false);
            await _daprClient.SaveStateAsync(_daprOptions.StateStore, storeKey, aggregateRoot, cancellationToken: cancellationToken).ConfigureAwait(false);
            return aggregateRoot;
        }

        public async Task<TAggregateRoot?> FirstOrDefaultWithCacheAsync(TKey id, CancellationToken cancellationToken = default)
        {
            string storeKey = id!.ToString()!;
            var aggregateRoot = await _daprClient.GetStateAsync<TAggregateRoot>(_daprOptions.StateStore, storeKey, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (aggregateRoot is not null)
                return aggregateRoot;
            aggregateRoot = await this.FirstOrDefaultAsync(id, cancellationToken).ConfigureAwait(false);
            if (aggregateRoot is not null)
                await _daprClient.SaveStateAsync(_daprOptions.StateStore, storeKey, aggregateRoot, cancellationToken: cancellationToken).ConfigureAwait(false);
            return aggregateRoot;
        }
 
        public async Task<TAggregateRoot> InsertWithCacheAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            aggregateRoot.AddDomainEvent(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, _daprOptions.StateStore!));
            aggregateRoot = await InsertAsync(aggregateRoot, autoSave, cancellationToken).ConfigureAwait(false);
            return aggregateRoot;
        }

        public async Task<TAggregateRoot> UpdateWithCacheAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            aggregateRoot.AddDomainEvent(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, _daprOptions.StateStore!));
            aggregateRoot = await UpdateAsync(aggregateRoot, autoSave, cancellationToken).ConfigureAwait(false);
            return aggregateRoot;
        }

        public async Task DeleteWithCacheAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            aggregateRoot.AddDomainEvent(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, _daprOptions.StateStore!));
            await DeleteAsync(aggregateRoot, autoSave, cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteWithCacheAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var aggregateRoot = await this.FirstAsync(id, cancellationToken).ConfigureAwait(false);
            aggregateRoot.AddDomainEvent(new TryDeleteCacheEventArgs<TKey>(aggregateRoot.Id, _daprOptions.StateStore!));
            await DeleteAsync(aggregateRoot, autoSave, cancellationToken).ConfigureAwait(false);
        }
    }
}