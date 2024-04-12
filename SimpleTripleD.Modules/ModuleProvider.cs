using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace SimpleTripleD.Modules
{
    internal class ModuleProvider
    {
        public (IEnumerable<DependencyModule> Modules, IEnumerable<Type> ModuleTypes) Tuples { get; }

        public ModuleProvider(DependencyModule applicationModule)
            => Tuples = GetDependencyModules(applicationModule);

        public async Task ConfigureServicesAsync(WebApplicationBuilder builder)
        {
            foreach (var module in Tuples.Modules)
                await module.ConfigureServicesAsync(builder).ConfigureAwait(false);
        }

        public async Task OnAppliactionInitalizationAsync(WebApplication app)
        {
            foreach (var module in Tuples.Modules)
                await module.OnApplicationInitializationAsync(app).ConfigureAwait(false);
        }

        private (IEnumerable<DependencyModule> Modules, IEnumerable<Type> ModuleTypes) GetDependencyModules(DependencyModule applicationModule)
        {
            var set = new HashSet<Type>();
            var list = new List<DependencyModule>();
            GetAllDependencyModules(applicationModule, list, set);
            return (list, set);
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
