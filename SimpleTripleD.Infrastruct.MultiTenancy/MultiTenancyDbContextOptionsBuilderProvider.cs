using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleTripleD.Infrastruct.EntityFrameworkCore;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public abstract class MultiTenancyDbContextOptionsBuilderProvider : IDbContextOptionsBuilderProvider
    {
        public async ValueTask ProvideDbContextOptionsBuilderAction(IServiceProvider provider, DbContextOptionsBuilder builder)
        {
            var scope = provider.CreateAsyncScope();
            await using (scope.ConfigureAwait(false))
            {
                var scopeProvider = scope.ServiceProvider;
                await ProvideDbContextOptionsBuilderAction(provider,
                    scopeProvider.GetRequiredService<ITenantProvider>(), builder).ConfigureAwait(false);
            }
            
        }

        protected abstract ValueTask ProvideDbContextOptionsBuilderAction(IServiceProvider provider, ITenantProvider tenantProvider, DbContextOptionsBuilder builder);
    }
}
