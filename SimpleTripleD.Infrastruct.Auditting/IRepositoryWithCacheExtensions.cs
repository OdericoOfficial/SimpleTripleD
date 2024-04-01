using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Infrastruct.Repositories;
using SimpleTripleD.Domain.Entities.Auditting;

namespace SimpleTripleD.Infrastruct.Auditting.Repository
{
    public static class IRepositoryWithCacheExtensions
    {
        public static Task<TAggregateRoot> InsertAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string storeName, string creatorId,
            TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.CreateTime = DateTime.UtcNow;
            aggregateRoot.CreatorId = creatorId;
            return repository.InsertAsync(storeName, aggregateRoot, cancellationToken);
        }

        public static Task<TAggregateRoot> UpdateAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string storeName, string lastModifierId,
            TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.LastModifiedTime = DateTime.UtcNow;
            aggregateRoot.LastModifierId = lastModifierId;
            return repository.UpdateAsync(storeName, aggregateRoot, cancellationToken);
        }

        public static Task DeleteAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string storeName, string deleterId,
            TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.IsDelete = true;
            aggregateRoot.DeleterId = deleterId;
            aggregateRoot.DeleteTime = DateTime.UtcNow;
            return repository.DeleteAsync(storeName, aggregateRoot, cancellationToken);
        }

        public static async Task DeleteAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string storeName, string deleterId,
            TKey id, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
            => await repository.DeleteAsync(storeName, deleterId, await repository.FirstAsync(id, cancellationToken).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);

        public static Task SoftDeleteAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string storeName, string deleterId,
            TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            aggregateRoot.IsDelete = true;
            aggregateRoot.DeleterId = deleterId;
            aggregateRoot.DeleteTime = DateTime.UtcNow;
            return repository.UpdateAsync(storeName, aggregateRoot, cancellationToken);
        }

        public static async Task SoftDeleteAsync<TAggregateRoot, TKey>(this IRepositoryWithCache<TAggregateRoot, TKey> repository, string storeName, string deleterId,
            TKey id, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
            => await repository.SoftDeleteAsync(storeName, deleterId, await repository.FirstAsync(id, cancellationToken).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
    }
}
