using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Infrastruct.MultiTenancy;

namespace SimpleTripleD.Infrastruct.Auditting
{
    public abstract class AudittingDbContext : MultiTenancyDbContext
    {
        private readonly IAudttingRecordFactory _factory;

        public virtual DbSet<AudittingRecord> AudittingRecords { get; set; }

        public AudittingDbContext(IAudttingRecordFactory factory, ITenantProvider provider, DbContextOptions options) : base(provider, options)
            => _factory = factory;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AudittingRecordConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.State == (EntityState.Deleted | EntityState.Modified | EntityState.Added)
                    && typeof(IAudittingObject).IsAssignableFrom(item.Entity.GetType()));
            _factory.AddEntityRecords(entities);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.State == (EntityState.Deleted | EntityState.Modified | EntityState.Added)
                    && typeof(IAudittingObject).IsAssignableFrom(item.Entity.GetType()));
            _factory.AddEntityRecords(entities);
            return base.SaveChangesAsync(cancellationToken);    
        }
    }
}
