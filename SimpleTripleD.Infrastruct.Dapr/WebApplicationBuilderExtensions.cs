using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using SimpleTripleD.Infrastruct.Dapr;

namespace Microsoft.AspNetCore.Builder
{
    internal static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddDapr(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<DaprOptions>(builder.Configuration.GetSection(nameof(DaprOptions)))
                .AddDaprClient();
            builder.Services.AddScoped<InvocationHandler>();

            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<DaprOptions>>();
                var daprClient = scope.ServiceProvider.GetRequiredService<DaprClient>();
                builder.Configuration.AddDaprSecretStore(options.Value.SecretStore!, daprClient);
            }
            return builder;
        }

        public static WebApplicationBuilder AddRestClient<TClient>(this WebApplicationBuilder builder, string clusterName, string controllerName) where TClient : class
        {
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<DaprOptions>>();
                builder.Services.AddHttpClient(clusterName)
                    .AddHttpMessageHandler<InvocationHandler>()
                    .AddTypedClient(client =>
                    {
                        client.BaseAddress = new Uri(options.Value.Routes![clusterName][controllerName]);
                        return RestService.For<TClient>(client);
                    });
                return builder;

            }
        }
    }
}
