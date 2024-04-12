using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    internal class Repository<TDbContext, TAggregateRoot> : ReadOnlyRepository<TDbContext, TAggregateRoot>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; }

        public Repository(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork)
            => UnitOfWork = unitOfWork;

        public async Task<TAggregateRoot> InsertAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var result = (await _unitOfWork.Context.Set<TAggregateRoot>()
                .AddAsync(aggregateRoot, cancellationToken).ConfigureAwait(false)).Entity;
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task InsertManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Context.Set<TAggregateRoot>()
                .AddRangeAsync(aggregateRoots).ConfigureAwait(false);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var result = _unitOfWork.Context.Set<TAggregateRoot>()
                .Update(aggregateRoot).Entity;
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task UpdateManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Context.Set<TAggregateRoot>()
                .UpdateRange(aggregateRoots);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Context.Set<TAggregateRoot>()
                .Remove(aggregateRoot);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _unitOfWork.Context.Set<TAggregateRoot>()
                .RemoveRange(aggregateRoots);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    internal class Repository<TDbContext, TAggregateRoot, TKey> : Repository<TDbContext, TAggregateRoot>, IRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        public Repository(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
            => await DeleteAsync(await this.FirstAsync(id, cancellationToken).ConfigureAwait(false), autoSave, cancellationToken).ConfigureAwait(false);

        public async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var id in ids)
                await DeleteAsync(id, autoSave: false, cancellationToken).ConfigureAwait(false);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}