using Microsoft.AspNetCore.Builder;

namespace SimpleTripleD.Modules
{
    public abstract class DependencyModule
    {
        private readonly List<OnConfigurationAssemblyScanning> _onConfigurationAssemblyScanning
            = new List<OnConfigurationAssemblyScanning>();

        private readonly List<OnInitialzationAssemblyScanning> _onInitialzationAssemblyScanning
            = new List<OnInitialzationAssemblyScanning>();

        public IEnumerable<OnConfigurationAssemblyScanning> OnConfigurationAssemblyScanning
            => _onConfigurationAssemblyScanning;

        public IEnumerable<OnInitialzationAssemblyScanning> OnInitialzationAssemblyScanning
            => _onInitialzationAssemblyScanning;

        public virtual void ConfigureServices(WebApplicationBuilder builder)
        { 
        }

        public virtual ValueTask ConfigureServicesAsync(WebApplicationBuilder builder)
        {
            ConfigureServices(builder);
            return ValueTask.CompletedTask;
        }

        public virtual void OnApplicationInitialization(WebApplication app)
        {

        }

        public virtual ValueTask OnApplicationInitializationAsync(WebApplication app)
        {
            OnApplicationInitialization(app);
            return ValueTask.CompletedTask;
        }

        protected void AddConfigurationAssemblyScanning(OnConfigurationAssemblyScanning onConfigurationAssemblyScanning)
            => _onConfigurationAssemblyScanning.Add(onConfigurationAssemblyScanning);

        protected void AddInitialzationAssemblyScanningEvent(OnInitialzationAssemblyScanning onInitialzationAssemblyScanning)
            => _onInitialzationAssemblyScanning.Add(onInitialzationAssemblyScanning);
    }
}
