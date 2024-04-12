using Microsoft.AspNetCore.Builder;

namespace SimpleTripleD.Modules
{
    internal class MultiModuleApplication : IMultiModuleApplication
    {
        private readonly ModuleProvider _moduleProvider;
        private readonly AssemblyScanner _assemblyScanner;

        public MultiModuleApplication(ModuleProvider moduleProvider, AssemblyScanner assemblyScanner) 
        {
            _moduleProvider = moduleProvider;
            _assemblyScanner = assemblyScanner;
        }

        public async Task ConfigureServicesAsync(WebApplicationBuilder builder)
        {
            await _moduleProvider.ConfigureServicesAsync(builder).ConfigureAwait(false);
            await _assemblyScanner.ConfigureServicesAsync(builder).ConfigureAwait(false);
        }

        public async Task OnAppliactionInitalizationAsync(WebApplication app)
        {
            await _moduleProvider.OnAppliactionInitalizationAsync(app).ConfigureAwait(false);
            await _assemblyScanner.OnApplicationInitializationAsync(app).ConfigureAwait(false); 
        }
    }
}
