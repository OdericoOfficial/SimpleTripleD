using Microsoft.Extensions.DependencyInjection;
using SimpleTripleD.Modules;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> OnApplicaitonInitializationAsync(this WebApplication app)
        {
            var moduleProvider = app.Services.GetRequiredService<IModuleProvider>();
            await moduleProvider.OnAppliactionInitalizationAsync(app).ConfigureAwait(false);
            return app;
        }
    }
}
