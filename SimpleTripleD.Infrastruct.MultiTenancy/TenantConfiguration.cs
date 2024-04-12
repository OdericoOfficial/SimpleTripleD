using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTripleD.Domain.MultiTenancy;
using SimpleTripleD.Infrastruct.Repositories;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    internal class TenantConfiguration : AggregateRootConfiguration<Tenant, Guid>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder);

            builder.Property("_name")
                .HasColumnName("Name")
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Property("_host")
                .HasColumnName("Host")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property("_connectionString")
                .HasColumnName("ConnectionString")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
