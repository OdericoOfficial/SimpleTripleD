using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleTripleD.Infrastruct.Auditting
{
    internal class AudittingRecordConfiguration : IEntityTypeConfiguration<AudittingRecord>
    {
        public void Configure(EntityTypeBuilder<AudittingRecord> builder)
        {
            builder.HasKey(item => item.Id);
            builder.Property(item => item.TableName)
                .HasMaxLength(20);
            builder.Property(item => item.OriginalValues)
                .HasMaxLength(100);
            builder.Property(item => item.NewValues)
                .HasMaxLength(100);
            builder.Property(item => item.EntityState)
                .HasMaxLength(10);
            builder.Property(item => item.CreatorId)
                .HasMaxLength(20);
            builder.Property(item => item.LastModifierId)
                .HasMaxLength(20);
            builder.Property(item => item.DeleterId)
                .HasMaxLength(20);
            builder.Property(item => item.PrimaryKey)
                .HasMaxLength(20);
        }
    }
}
