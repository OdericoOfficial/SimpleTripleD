using System.Reflection;

namespace SimpleTripleD.Modules
{
    internal static class MultiModuleApplicationFactory
    {
        public static IMultiModuleApplication CreateDefaultApplication<TModule>()
            where TModule : DependencyModule, new()
        {
            var moduleProvider = new ModuleProvider(new TModule());
            var assemblyScanner = new AssemblyScanner(moduleProvider.Tuples.ModuleTypes
                .Where(item => item.GetCustomAttribute<AssemblyEntryAttribute>() is not null)
                .SelectMany(item => item.Assembly.GetTypes()));
            return new MultiModuleApplication(moduleProvider, assemblyScanner);
        }
    }
}
