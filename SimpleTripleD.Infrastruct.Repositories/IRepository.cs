using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>, IUnitOfWorkAccessor<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> InsertAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task InsertManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task UpdateManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task DeleteAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task DeleteManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default);
    }

    public interface IRepository<TAggregateRoot, TKey> : IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
        
        Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}