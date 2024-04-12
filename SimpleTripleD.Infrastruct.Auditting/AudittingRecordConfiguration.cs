using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Infrastruct.MultiTenancy;

namespace SimpleTripleD.Infrastruct.Auditting
{
    internal class AudittingRecordConfiguration : MultiTenancyConfiguration<AudittingRecord, Guid>
    {
        public override void Configure(EntityTypeBuilder<AudittingRecord> builder)
        {
            base.Configure(builder);
            builder.OwnsMany(item => item.EntityChangeRecords)
                .ToJson();
            builder.OwnsOne(item => item.ExecutingMethodRecord)
                .ToJson();
        }
    }
}
