using Microsoft.AspNetCore.Builder;

namespace SimpleTripleD.Modules
{
    public interface IModuleProvider
    {
        IEnumerable<DependencyModule> Modules { get; }

        Task ConfigureServicesAsync(WebApplicationBuilder builder);
        
        Task OnAppliactionInitalizationAsync(WebApplication app);
    }
}
