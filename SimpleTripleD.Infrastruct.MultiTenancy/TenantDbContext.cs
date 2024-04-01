using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities.MultiTenancy;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public class TenantDbContext : DbContext
    {
        public virtual DbSet<Tenant> Tenants { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TanentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
