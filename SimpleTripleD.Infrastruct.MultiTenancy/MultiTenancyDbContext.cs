using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.MultiTenancy;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public abstract class MultiTenancyDbContext : DbContext
    {
        private readonly ITenantProvider _provider;

        public MultiTenancyDbContext(ITenantProvider provider, DbContextOptions options) : base(options)
            => _provider = provider;

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.State == EntityState.Added && typeof(IMultiTenancyObject).IsAssignableFrom(item.Entity.GetType()));
            foreach (var item in entities)
                ((IMultiTenancyObject)item.Entity).TenantId = _provider.GetTenantId();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.State == EntityState.Added && typeof(IMultiTenancyObject).IsAssignableFrom(item.Entity.GetType()));
            foreach (var item in entities)
                ((IMultiTenancyObject)item.Entity).TenantId = await _provider.GetTenantIdAsync().ConfigureAwait(false);
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
