using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    internal abstract class ReadOnlyRepository<TDbContext, TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {
        protected readonly IUnitOfWork<TDbContext> _unitOfWork;

        public ReadOnlyRepository(IUnitOfWork<TDbContext> unitOfWork)
            => _unitOfWork = unitOfWork;

        public IQueryable<TAggregateRoot> AsQueryable()
            => _unitOfWork.Context.Set<TAggregateRoot>().AsNoTracking();
    }

    internal abstract class ReadOnlyRepository<TDbContext, TAggregateRoot, TKey> : ReadOnlyRepository<TDbContext, TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        public ReadOnlyRepository(IUnitOfWork<TDbContext> context) : base(context)
        {
        }
    }
}