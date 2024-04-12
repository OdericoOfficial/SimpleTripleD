using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleTripleD.Modules;

namespace SimpleTripleD.Infrastruct.Auditting
{
    public class AudittingInfrastructModule : DependencyModule
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
            => builder.Services.TryAddScoped<IAudttingRecordFactory, AudittingRecordFactory>();
    }
}
