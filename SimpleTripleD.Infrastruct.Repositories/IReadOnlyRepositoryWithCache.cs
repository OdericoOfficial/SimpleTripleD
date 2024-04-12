using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IReadOnlyRepositoryWithCache<TAggregateRoot, TKey> : IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        Task<TAggregateRoot> FirstWithCacheAsync(TKey id, CancellationToken cancellationToken = default);

        Task<TAggregateRoot?> FirstOrDefaultWithCacheAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
