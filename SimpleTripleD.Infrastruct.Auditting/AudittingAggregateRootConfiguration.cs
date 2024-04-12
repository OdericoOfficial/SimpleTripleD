using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Infrastruct.MultiTenancy;
using System.Linq.Expressions;

namespace SimpleTripleD.Infrastruct.Auditting
{
    public abstract class AudittingAggregateRootConfiguration<TAggregateRoot> : MultiTenancyConfiguration<TAggregateRoot>
        where TAggregateRoot : AudittingAggregateRoot
    {
        public override void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            base.Configure(builder);
            builder.Property(item => item.CreateTime)
                .IsRequired();
            builder.Property(item => item.CreatorId)
                .HasMaxLength(25)
                .IsRequired();
            builder.Property(item => item.DeleteTime);
            builder.Property(item => item.DeleterId)
                .HasMaxLength(25);
            builder.Property(item => item.IsDelete)
                .IsRequired();
            builder.Property(item => item.LastModifiedTime);
            builder.Property(item => item.LastModifierId)
                .HasMaxLength(25);
        }
    }

    public abstract class AudittingAggregateRootConfiguration<TAggregateRoot, TKey> : MultiTenancyConfiguration<TAggregateRoot, TKey>
        where TAggregateRoot : AudittingAggregateRoot<TKey>
    {
        public override void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            base.Configure(builder);
            builder.Property(item => item.CreateTime)
                .IsRequired();
            builder.Property(item => item.CreatorId)
                .HasMaxLength(25)
                .IsRequired();
            builder.Property(item => item.DeleteTime);
            builder.Property(item => item.DeleterId)
                .HasMaxLength(25);
            builder.Property(item => item.IsDelete)
                .IsRequired();
            builder.Property(item => item.LastModifiedTime);
            builder.Property(item => item.LastModifierId)
                .HasMaxLength(25);
        }
    }
}
