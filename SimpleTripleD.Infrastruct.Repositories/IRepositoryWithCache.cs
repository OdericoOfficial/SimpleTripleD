using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IRepositoryWithCache<TAggregateRoot, TKey> : IReadOnlyRepositoryWithCache<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
    {        
        Task<TAggregateRoot> InsertWithCacheAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task<TAggregateRoot> UpdateWithCacheAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task DeleteWithCacheAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task DeleteWithCacheAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}