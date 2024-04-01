using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using SimpleTripleD.Infrastruct.Dapr;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApplicationBuilderExtensions
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

        public static WebApplicationBuilder AddDaprClient<TClient>(this WebApplicationBuilder builder, string clusterName) where TClient : class
        {
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<DaprOptions>>();
                builder.Services.AddHttpClient(clusterName)
                    .AddHttpMessageHandler<InvocationHandler>()
                    .AddTypedClient(client =>
                    {
                        client.BaseAddress = new Uri(options.Value.EndPoints![clusterName]);
                        return RestService.For<TClient>(client);
                    });
                return builder;

            }
        }
    }
}
