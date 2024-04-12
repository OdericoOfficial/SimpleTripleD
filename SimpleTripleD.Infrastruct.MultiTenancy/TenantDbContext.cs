using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.MultiTenancy;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    internal class TenantDbContext : DbContext
    {
        public virtual DbSet<Tenant> Tenants { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
