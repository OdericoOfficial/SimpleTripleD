using Microsoft.AspNetCore.Builder;
using SimpleTripleD.Modules;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleTripleD.Infrastruct.Dapr
{
    public class DaprInfrastructModule : DependencyModule
    {
        private readonly List<(Type restType, string clusterName, string controllerName)> _restClients
            = new List<(Type restType, string clusterName, string controllerName)>();

        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.AddDapr();
            OnConfigurationAssemblyScanning += (type, builder) =>
            {
                var attribute = type.GetCustomAttribute<DaprInvocationAttribute>();
                if (attribute is not null)
                    _restClients.Add((type, attribute.ClusterName, attribute.ControllerName));
                return ValueTask.CompletedTask;
            };

            OnConfigurationAssemblyScanned += builder =>
            {
                var methodInfo = typeof(DaprInfrastructModule).GetMethod("AddRestClients", BindingFlags.Static | BindingFlags.NonPublic);
                if (methodInfo is null)
                    throw new InvalidOperationException("Cannot find method.");

                var builderParameter = Expression.Parameter(typeof(WebApplicationBuilder));
                var clusterNameParameter = Expression.Parameter(typeof(string));
                var controllerNameParameter = Expression.Parameter(typeof(string));

                foreach ((var restType, var clusterName, var controllerName) in _restClients)
                {
                    var genericMethod = methodInfo.MakeGenericMethod(restType);
                    var call = Expression.Call(genericMethod, builderParameter, clusterNameParameter, controllerNameParameter);
                    Expression.Lambda<Action<WebApplicationBuilder, string, string>>(call, builderParameter, clusterNameParameter, controllerNameParameter)
                        .Compile()
                        .Invoke(builder, clusterName, controllerName);
                }

                return ValueTask.CompletedTask;
            };
        }

        private static void AddRestClients<TClient>(WebApplicationBuilder builder, string clusterName, string controllerName)
            where TClient : class
            => builder.AddRestClient<TClient>(clusterName, controllerName);
    }
}
