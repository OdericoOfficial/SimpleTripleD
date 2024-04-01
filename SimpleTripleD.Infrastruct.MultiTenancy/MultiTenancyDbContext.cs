using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities.MultiTenancy;
using SimpleTripleD.Infrastruct.Auditting;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public abstract class MultiTenancyDbContext : AudittingDbContext
    {
        private readonly ITenantProvider _provider;

        public MultiTenancyDbContext(ITenantProvider provider, DbContextOptions options) : base(options)
            => _provider = provider;

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.State == EntityState.Added && item.Entity.GetType().BaseType == typeof(IMultiTenancyObject));
            foreach (var item in entities)
                ((IMultiTenancyObject)item.Entity).TenantId = _provider.GetTenantId();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.State == EntityState.Added && item.Entity.GetType().BaseType == typeof(IMultiTenancyObject));
            foreach (var item in entities)
                ((IMultiTenancyObject)item.Entity).TenantId = await _provider.GetTenantIdAsync().ConfigureAwait(false);
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
