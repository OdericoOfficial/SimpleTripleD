using Microsoft.Extensions.DependencyInjection;
using SimpleTripleD.Modules;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApplicationBuilderExtensions
    {
        public static async Task<WebApplicationBuilder> ConfigureServicesAsync<TModule>(this WebApplicationBuilder builder)
            where TModule : DependencyModule, new()
        {
            var moduleProvider = new ModuleProvider<TModule>();
            builder.Services.AddSingleton<IModuleProvider>(moduleProvider);
            await moduleProvider.ConfigureServicesAsync(builder).ConfigureAwait(false);
            return builder;
        }
    }
}
