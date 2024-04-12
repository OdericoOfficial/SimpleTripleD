using Microsoft.Extensions.DependencyInjection;
using SimpleTripleD.Modules;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApplicationBuilderExtensions
    {
        public static async Task<WebApplicationBuilder> ConfigureServicesAsync<TModule>(this WebApplicationBuilder builder)
            where TModule : DependencyModule, new()
        {
            var multiModuleApplication = MultiModuleApplicationFactory.CreateDefaultApplication<TModule>();
            builder.Services.AddSingleton(multiModuleApplication);
            await multiModuleApplication.ConfigureServicesAsync(builder).ConfigureAwait(false);
            return builder;
        }
    }
}
