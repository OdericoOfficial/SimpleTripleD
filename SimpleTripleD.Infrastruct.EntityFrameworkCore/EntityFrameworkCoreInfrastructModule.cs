using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleTripleD.Modules;

namespace SimpleTripleD.Infrastruct.EntityFrameworkCore
{
    public class EntityFrameworkCoreInfrastructModule<TDbContext> : DependencyModule
        where TDbContext : DbContext
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
            => builder.Services.AddDbContext<TDbContext>(async (provider, builder) =>
                await provider.GetRequiredKeyedService<IDbContextOptionsBuilderProvider>(typeof(TDbContext))
                    .ProvideDbContextOptionsBuilderAction(provider, builder).ConfigureAwait(false));
    }
}
