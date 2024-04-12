using Microsoft.AspNetCore.Builder;

namespace SimpleTripleD.Modules
{
    public abstract class DependencyModule
    {
        public event OnConfigurationAssemblyScanning? OnConfigurationAssemblyScanning
        {
            add => AssemblyScanner.OnConfigurationAssemblyScanning += value;
            remove => AssemblyScanner.OnConfigurationAssemblyScanning -= value;
        }

        public event OnConfigurationAssemblyScanned? OnConfigurationAssemblyScanned
        {
            add => AssemblyScanner.OnConfigurationAssemblyScanned += value;
            remove => AssemblyScanner.OnConfigurationAssemblyScanned -= value;
        }

        public event OnInitialzationAssemblyScanning? OnInitialzationAssemblyScanning
        {
            add => AssemblyScanner.OnInitialzationAssemblyScanning += value;
            remove => AssemblyScanner.OnInitialzationAssemblyScanning -= value;
        }

        public event OnInitialzationAssemblyScanned? OnInitialzationAssemblyScanned
        { 
            add => AssemblyScanner.OnInitialzationAssemblyScanned += value;
            remove => AssemblyScanner.OnInitialzationAssemblyScanned -= value;
        }

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
    }
}
