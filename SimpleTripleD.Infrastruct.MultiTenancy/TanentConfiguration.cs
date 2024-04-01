using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTripleD.Domain.Entities.MultiTenancy;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    internal class TanentConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(item => item.Id);
            builder.Property(item => item.Name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(item => item.Host)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(item => item.ConnectionString)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
