using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace SimpleTripleD.Modules
{
    internal class AssemblyScanner : IAssemblyScanner
    {
        private readonly IEnumerable<Type> _exportTypes;
        public event OnConfigurationAssemblyScanning? OnConfigurationAssemblyScanning;
        public event OnInitialzationAssemblyScanning? OnInitialzationAssemblyScanning;

        public AssemblyScanner() 
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly is null)
                throw new NullReferenceException("Cannot find entry assembly.");
            _exportTypes = assembly.GetExportedTypes();
        }

        public async Task ScanInConfigurationAsync()
        {
            if (OnConfigurationAssemblyScanning is null)
                return;

            foreach (var exportType in _exportTypes)
                await OnConfigurationAssemblyScanning.Invoke(exportType).ConfigureAwait(false);
        }

        public async Task ScanInInitializationAsync()
        {
            if (OnInitialzationAssemblyScanning is null) 
                return;

            foreach (var exportType in _exportTypes)
                await OnInitialzationAssemblyScanning.Invoke(exportType).ConfigureAwait(false);
        }
    }
}
