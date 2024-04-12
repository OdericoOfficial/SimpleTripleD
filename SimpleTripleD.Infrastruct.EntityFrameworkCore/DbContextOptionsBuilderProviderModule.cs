using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleTripleD.Modules;

namespace SimpleTripleD.Infrastruct.EntityFrameworkCore
{
    public class DbContextOptionsBuilderProviderModule<TDbContext, TDbContextOptionBuilderProvider> : DependencyModule
        where TDbContext : DbContext
        where TDbContextOptionBuilderProvider : IDbContextOptionsBuilderProvider, new()
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
            => builder.Services.TryAddKeyedSingleton<IDbContextOptionsBuilderProvider>(typeof(TDbContext), new TDbContextOptionBuilderProvider());
    }
}
