using Microsoft.AspNetCore.Builder;

namespace SimpleTripleD.Modules
{
    public interface IMultiModuleApplication
    {
        Task ConfigureServicesAsync(WebApplicationBuilder builder);

        Task OnAppliactionInitalizationAsync(WebApplication app);

    }
}
