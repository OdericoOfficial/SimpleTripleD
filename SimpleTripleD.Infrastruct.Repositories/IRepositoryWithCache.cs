using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IRepositoryWithCache<TAggregateRoot, TKey> : IRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        Task<TAggregateRoot> FirstAsync(string storeName, TKey id, CancellationToken cancellationToken = default);
        Task<TAggregateRoot?> FirstOrDefaultAsync(string storeName, TKey id, CancellationToken cancellationToken = default);
        Task<TAggregateRoot> InsertAsync(string storeName, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task<TAggregateRoot> UpdateAsync(string storeName, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task DeleteAsync(string storeName, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task DeleteAsync(string storeName, TKey id, CancellationToken cancellationToken = default);
    }
}