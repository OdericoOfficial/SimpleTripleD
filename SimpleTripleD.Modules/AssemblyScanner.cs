using Microsoft.AspNetCore.Builder;

namespace SimpleTripleD.Modules
{
    public delegate ValueTask OnConfigurationAssemblyScanning(Type type, WebApplicationBuilder builder);

    public delegate ValueTask OnConfigurationAssemblyScanned(WebApplicationBuilder builder);

    public delegate ValueTask OnInitialzationAssemblyScanning(Type type, WebApplication app);

    public delegate ValueTask OnInitialzationAssemblyScanned(WebApplication app);

    internal class AssemblyScanner
    {
        private readonly IEnumerable<Type> _types;
        
        public static event OnConfigurationAssemblyScanning? OnConfigurationAssemblyScanning;

        public static event OnConfigurationAssemblyScanned? OnConfigurationAssemblyScanned;

        public static event OnInitialzationAssemblyScanning? OnInitialzationAssemblyScanning;

        public static event OnInitialzationAssemblyScanned? OnInitialzationAssemblyScanned;

        public AssemblyScanner(IEnumerable<Type> types) 
            => _types = types;

        public async Task ConfigureServicesAsync(WebApplicationBuilder builder)
        {
            if (OnConfigurationAssemblyScanning is null)
                return;

            foreach (var type in _types)
                await OnConfigurationAssemblyScanning.Invoke(type, builder).ConfigureAwait(false);

            if (OnConfigurationAssemblyScanned is not null)
                await OnConfigurationAssemblyScanned.Invoke(builder).ConfigureAwait(false);
        }

        public async Task OnApplicationInitializationAsync(WebApplication app)
        {
            if (OnInitialzationAssemblyScanning is null) 
                return;

            foreach (var type in _types)
                await OnInitialzationAssemblyScanning.Invoke(type, app).ConfigureAwait(false);
        
            if (OnInitialzationAssemblyScanned is not null)
                await OnInitialzationAssemblyScanned.Invoke(app).ConfigureAwait(false);
        }
    }
}
