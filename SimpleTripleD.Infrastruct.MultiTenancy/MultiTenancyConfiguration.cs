using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTripleD.Domain.MultiTenancy;
using SimpleTripleD.Infrastruct.Repositories;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public abstract class MultiTenancyConfiguration<TAggregateRoot> : AggregateRootConfiguration<TAggregateRoot>
        where TAggregateRoot : MultiTenancyAggregateRoot
    {
        public override void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            base.Configure(builder);
            builder.Property(entity => entity.TenantId)
                .IsRequired();
        }
    }

    public abstract class MultiTenancyConfiguration<TAggregateRoot, TKey> : AggregateRootConfiguration<TAggregateRoot, TKey>
        where TAggregateRoot : MultiTenancyAggregateRoot<TKey>
    {
        public override void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            base.Configure(builder);
            builder.Property(entity => entity.TenantId)
                .IsRequired();
        }
    }
}
