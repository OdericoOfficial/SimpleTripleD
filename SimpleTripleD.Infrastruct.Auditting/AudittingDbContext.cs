using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.Entities.Auditting;

namespace SimpleTripleD.Infrastruct.Auditting
{
    public abstract class AudittingDbContext : DbContext
    {
        private List<AudittingEntry>? _entries;

        internal virtual DbSet<AudittingRecord> AudittingRecords { get; set; }

        public AudittingDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AudittingRecordConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            PreSaveChanges();
            var result = base.SaveChanges();
            PostSaveChanges();
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PreSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false); 
            await PostSaveChangesAsync().ConfigureAwait(false);
            return result;
        }
    
        private void PreSaveChanges()
        {
            if (_entries is null)
                _entries = new List<AudittingEntry>();
            else
                _entries.Clear();

            foreach (var entry in ChangeTracker.Entries().AsParallel())
            {
                if (entry.State == EntityState.Detached 
                    || entry.State == EntityState.Unchanged
                        || entry.Entity is not IAudittingObject obj)
                    continue;
                _entries.Add(new AudittingEntry(entry, obj));
            }
        }

        private void PostSaveChanges()
        {
            if (_entries is null)
                return;

            AudittingRecords.AddRange(_entries.Select(item => new AudittingRecord(item)));
            base.SaveChanges();
        }

        private async Task PostSaveChangesAsync()
        {
            if (_entries is null)
                return;

            await AudittingRecords.AddRangeAsync(_entries.Select(item => new AudittingRecord(item))).ConfigureAwait(false);
            await base.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
