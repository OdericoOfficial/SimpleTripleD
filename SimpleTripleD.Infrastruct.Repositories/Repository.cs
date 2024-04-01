using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public class Repository<TDbContext, TAggregateRoot> : ReadOnlyRepository<TDbContext, TAggregateRoot>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; }

        public Repository(IUnitOfWork unitOfWork, TDbContext context) : base(context)
            => UnitOfWork = unitOfWork;

        public async Task<TAggregateRoot> InsertAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var result = (await _context.Set<TAggregateRoot>()
                .AddAsync(aggregateRoot, cancellationToken).ConfigureAwait(false)).Entity;
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task InsertManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _context.Set<TAggregateRoot>()
                .AddRangeAsync(aggregateRoots).ConfigureAwait(false);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var result = _context.Set<TAggregateRoot>()
                .Update(aggregateRoot).Entity;
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        public async Task UpdateManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _context.Set<TAggregateRoot>()
                .UpdateRange(aggregateRoots);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(TAggregateRoot aggregateRoot, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _context.Set<TAggregateRoot>()
                .Remove(aggregateRoot);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteManyAsync(IEnumerable<TAggregateRoot> aggregateRoots, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _context.Set<TAggregateRoot>()
                .RemoveRange(aggregateRoots);
            if (autoSave)
                await UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public class Repository<TDbContext, TAggregateRoot, TKey> : Repository<TDbContext, TAggregateRoot>, IRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        public Repository(IUnitOfWork unitOfWork, TDbContext context) : base(unitOfWork, context)
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