using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Infrastruct.Repositories;

namespace SimpleTripleD.Infrastruct.Auditting.Repository
{
    public static class IRepositoryExtensions
    {
        public static Task<TAggregateRoot> InsertAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, TAggregateRoot aggregateRoot, string creatorId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            aggregateRoot.CreateTime = DateTime.UtcNow;
            aggregateRoot.CreatorId = creatorId;
            return repository.InsertAsync(aggregateRoot, autoSave, cancellationToken);
        }
    
        public static Task InsertManyAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, IEnumerable<TAggregateRoot> aggregateRoots, string creatorId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            var time = DateTime.UtcNow;
            foreach (var aggregateRoot in aggregateRoots)
            {
                aggregateRoot.CreateTime = time;
                aggregateRoot.CreatorId = creatorId;
            }
            return repository.InsertManyAsync(aggregateRoots, autoSave, cancellationToken);
        }

        public static Task<TAggregateRoot> UpdateAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, TAggregateRoot aggregateRoot, string lastModifierId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            aggregateRoot.LastModifiedTime = DateTime.UtcNow;
            aggregateRoot.LastModifierId = lastModifierId;
            return repository.UpdateAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static Task UpdataManyAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, IEnumerable<TAggregateRoot> aggregateRoots, string lastModifierId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            var time = DateTime.UtcNow;
            foreach (var aggregateRoot in aggregateRoots)
            {
                aggregateRoot.LastModifiedTime = time;
                aggregateRoot.LastModifierId = lastModifierId;
            }
            return repository.UpdateManyAsync(aggregateRoots, autoSave, cancellationToken);
        }

        public static Task DeleteAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, TAggregateRoot aggregateRoot, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            aggregateRoot.DeleteTime = DateTime.UtcNow;
            aggregateRoot.DeleterId = deleterId;
            aggregateRoot.IsDelete = true;
            return repository.DeleteAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static Task DeleteManyAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, IEnumerable<TAggregateRoot> aggregateRoots, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            var time = DateTime.UtcNow;
            foreach (var aggregateRoot in aggregateRoots)
            {
                aggregateRoot.DeleteTime = time;
                aggregateRoot.DeleterId = deleterId;
                aggregateRoot.IsDelete = true;
            }
            return repository.DeleteManyAsync(aggregateRoots, autoSave, cancellationToken);
        }

        public static async Task DeleteAsync<TAggregateRoot, TKey>(this IRepository<TAggregateRoot, TKey> repository, TKey id, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
            => await repository.DeleteAsync(await repository.FirstAsync(id, cancellationToken).ConfigureAwait(false), deleterId, autoSave, cancellationToken).ConfigureAwait(false);

        public static async Task DeleteManyAsync<TAggregateRoot, TKey>(this IRepository<TAggregateRoot, TKey> repository, IEnumerable<TKey> ids, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            foreach (var id in ids)
                await repository.DeleteAsync(id, deleterId, false, cancellationToken).ConfigureAwait(false);
            if (autoSave)
                await repository.UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public static Task SoftDeleteAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, TAggregateRoot aggregateRoot, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            aggregateRoot.DeleteTime = DateTime.UtcNow;
            aggregateRoot.DeleterId = deleterId;
            aggregateRoot.IsDelete = true;
            return repository.UpdateAsync(aggregateRoot, autoSave, cancellationToken);
        }

        public static Task SoftDeleteManyAsync<TAggregateRoot>(this IRepository<TAggregateRoot> repository, IEnumerable<TAggregateRoot> aggregateRoots, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot, IAudittingObject
        {
            var time = DateTime.UtcNow;
            foreach (var aggregateRoot in aggregateRoots)
            {
                aggregateRoot.DeleteTime = time;
                aggregateRoot.DeleterId = deleterId;
                aggregateRoot.IsDelete = true;
            }
            return repository.UpdateManyAsync(aggregateRoots, autoSave, cancellationToken);
        }

        public static async Task SoftDeleteAsync<TAggregateRoot, TKey>(this IRepository<TAggregateRoot, TKey> repository, TKey id, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
            => await repository.SoftDeleteAsync(await repository.FirstAsync(id, cancellationToken).ConfigureAwait(false), deleterId, autoSave, cancellationToken);

        public static async Task SoftDeleteManyAsync<TAggregateRoot, TKey>(this IRepository<TAggregateRoot, TKey> repository, IEnumerable<TKey> ids, string deleterId,
            bool autoSave = false, CancellationToken cancellationToken = default) where TAggregateRoot : IAggregateRoot<TKey>, IAudittingObject
        {
            foreach (var id in ids)
                await repository.SoftDeleteAsync(id, deleterId, false, cancellationToken).ConfigureAwait(false);
            if (autoSave)
                await repository.UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
