using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public abstract class AggregateRootConfiguration<TAggregateRoot> : IEntityTypeConfiguration<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        public virtual void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            builder.Ignore("_domainEvents");
            builder.Ignore("_integrationEvents");
        }
    }

    public abstract class AggregateRootConfiguration<TAggregateRoot, TKey> : IEntityTypeConfiguration<TAggregateRoot>
        where TAggregateRoot : AggregateRoot<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            builder.Ignore("_domainEvents");
            builder.Ignore("_integrationEvents");
            builder.HasKey("_id")
                .HasName("Id");
        }
    }
}
