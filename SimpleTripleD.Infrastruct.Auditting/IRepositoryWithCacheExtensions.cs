using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Infrastruct.Repositories;

namespace SimpleTripleD.Infrastruct.Auditting.Repository
{
    public static class IRepositoryWithCacheExtensions
    {
        public static Task<TAggregateRoot> InsertWithCacheAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string creatorId,
            TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.CreateTime = DateTime.UtcNow;
            aggregateRoot.CreatorId = creatorId;
            return repository.InsertWithCacheAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static Task<TAggregateRoot> UpdateWithCacheAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string lastModifierId,
            TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.LastModifiedTime = DateTime.UtcNow;
            aggregateRoot.LastModifierId = lastModifierId;
            return repository.UpdateWithCacheAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static Task DeleteWithCacheAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string deleterId,
            TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.IsDelete = true;
            aggregateRoot.DeleterId = deleterId;
            aggregateRoot.DeleteTime = DateTime.UtcNow;
            return repository.DeleteWithCacheAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static async Task DeleteWithCacheAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string deleterId,
            TKey id, bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
            => await repository.DeleteWithCacheAsync(deleterId, await repository.FirstAsync(id, cancellationToken).ConfigureAwait(false), autoSave, cancellationToken).ConfigureAwait(false);

        public static Task SoftDeleteWithCacheAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string deleterId,
            TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.IsDelete = true;
            aggregateRoot.DeleterId = deleterId;
            aggregateRoot.DeleteTime = DateTime.UtcNow;
            return repository.UpdateWithCacheAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static async Task SoftDeleteWithCacheAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string deleterId,
            TKey id, bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
            => await repository.SoftDeleteWithCacheAsync(deleterId, await repository.FirstAsync(id, cancellationToken).ConfigureAwait(false), autoSave, cancellationToken).ConfigureAwait(false);
    }
}
