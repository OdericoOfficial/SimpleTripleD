using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleTripleD.Domain.MultiTenancy;
using SimpleTripleD.Infrastruct.Dapr;
using SimpleTripleD.Infrastruct.EntityFrameworkCore;
using SimpleTripleD.Infrastruct.Repositories;
using SimpleTripleD.Modules;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    [DependencyModules(typeof(DaprInfrastructModule),
        typeof(EntityFrameworkCoreInfrastructModule<TenantDbContext>),
        typeof(RepositoriesInfrastructModule<TenantDbContext, Tenant, Guid>))]
    public class MultiTenancyInfrastructModule<TDbContextOptionsBuilderProvider> : DependencyModule
        where TDbContextOptionsBuilderProvider : IDbContextOptionsBuilderProvider, new()
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.TryAddKeyedSingleton<IDbContextOptionsBuilderProvider>(typeof(TenantDbContext), new TDbContextOptionsBuilderProvider());
            builder.Services.TryAddScoped<ITenantProvider, TenantProvider>();
        }
    }
}
