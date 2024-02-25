using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Repositories
{
    public class ReadOnlyRepository<TDbContext, TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {
        protected readonly TDbContext _context;

        public ReadOnlyRepository(TDbContext context)
            => _context = context;

        public IQueryable<TAggregateRoot> AsQueryable()
            => _context.Set<TAggregateRoot>();
    }

    public class ReadOnlyRepository<TDbContext, TAggregateRoot, TKey> : ReadOnlyRepository<TDbContext, TAggregateRoot>, IReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot>
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        public ReadOnlyRepository(TDbContext context) : base(context)
        {
        }
    }
}