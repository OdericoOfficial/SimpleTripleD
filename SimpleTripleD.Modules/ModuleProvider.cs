using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace SimpleTripleD.Modules
{
    internal class ModuleProvider<TModule> : IModuleProvider
        where TModule : DependencyModule, new()
    {
        private readonly IAssemblyScanner _scanner = new AssemblyScanner();

        public IEnumerable<DependencyModule> Modules { get; }

        public ModuleProvider() 
            => Modules = GetDependencyModules();

        public async Task ConfigureServicesAsync(WebApplicationBuilder builder)
        {
            foreach (var module in Modules)
            {
                await module.ConfigureServicesAsync(builder).ConfigureAwait(false);
                foreach (var item in module.OnConfigurationAssemblyScanning)
                    _scanner.OnConfigurationAssemblyScanning += item;
            }

            await _scanner.ScanInConfigurationAsync().ConfigureAwait(false);
        }

        public async Task OnAppliactionInitalizationAsync(WebApplication app)
        {
            foreach (var module in Modules)
            {
                await module.OnApplicationInitializationAsync(app).ConfigureAwait(false);
                foreach (var item in module.OnInitialzationAssemblyScanning)
                    _scanner.OnInitialzationAssemblyScanning += item;
            }
        
            await _scanner.ScanInInitializationAsync().ConfigureAwait(false);
        }

        private IEnumerable<DependencyModule> GetDependencyModules()
        {
            var set = new HashSet<Type>();
            var list = new List<DependencyModule>();
            var module = new TModule();
            GetAllDependencyModules(module, list, set);
            return list;
        }

        private void GetAllDependencyModules(DependencyModule current, List<DependencyModule> modules, HashSet<Type> set)
        {
            modules.Add(current);

            var type = current.GetType();
            if (set.Contains(type))
                return;

            var attribute = type.GetCustomAttribute<DependencyModulesAttribute>();
            if (attribute is null)
                return;

            foreach (var moduleType in attribute.ModuleTypes)
            {
                var module = Activator.CreateInstance(moduleType);
                if (module is null && module is DependencyModule dependencyModule)
                    GetAllDependencyModules(dependencyModule, modules, set);
            }
        }
    }
}
