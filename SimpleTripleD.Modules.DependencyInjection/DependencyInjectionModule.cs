using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace SimpleTripleD.Modules.DependencyInjection
{
    public class DependencyInjectionModule : DependencyModule
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            OnConfigurationAssemblyScanning += (type, builder) =>
            {
                foreach (var attribute in type.GetCustomAttributes()
                    .Where(item => item is LifeCircleAttribute)
                    .Select(item => (LifeCircleAttribute)item))
                {
                    switch (attribute.LifeCircle)
                    {
                        case LifeCircle.Transient:
                            if (attribute.ServiceType is null)
                            {
                                if (attribute.ServiceKey is null)
                                    builder.Services.TryAddTransient(type);
                                else
                                    builder.Services.TryAddKeyedTransient(type, attribute.ServiceKey);
                            }
                            else
                            {
                                if (attribute.ServiceKey is null)
                                    builder.Services.TryAddTransient(attribute.ServiceType, type);
                                else
                                    builder.Services.TryAddKeyedTransient(attribute.ServiceType, attribute.ServiceKey, type);
                            }
                            break;
                        case LifeCircle.Scoped:
                            if (attribute.ServiceType is null)
                            {
                                if (attribute.ServiceKey is null)
                                    builder.Services.TryAddScoped(type);
                                else
                                    builder.Services.TryAddKeyedScoped(type, attribute.ServiceKey);
                            }
                            else
                            {
                                if (attribute.ServiceKey is null)
                                    builder.Services.TryAddScoped(attribute.ServiceType, type);
                                else
                                    builder.Services.TryAddKeyedScoped(attribute.ServiceType, attribute.ServiceKey, type);
                            }
                            break;
                        case LifeCircle.Singleton:
                            if (attribute.ServiceType is null)
                            {
                                if (attribute.ServiceKey is null)
                                    builder.Services.TryAddSingleton(type);
                                else
                                    builder.Services.TryAddKeyedSingleton(service: type, serviceKey: attribute.ServiceKey);
                            }
                            else
                            {
                                if (attribute.ServiceKey is null)
                                    builder.Services.TryAddSingleton(attribute.ServiceType, type);
                                else
                                    builder.Services.TryAddKeyedSingleton(attribute.ServiceType, attribute.ServiceKey, type);
                            }
                            break;
                    }
                }
                return ValueTask.CompletedTask;
            };
        }
    }
}
