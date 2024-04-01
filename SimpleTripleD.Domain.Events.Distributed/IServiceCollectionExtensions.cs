using MediatR;
using SimpleTripleD.Domain.Events.Distributed;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedEventHandler<TIntegrationEventArgs>(this IServiceCollection services)
            where TIntegrationEventArgs : IntegrationEventArgs
            => services.AddScoped<INotificationHandler<TIntegrationEventArgs>, IntegrationEventHandler<TIntegrationEventArgs>>();
    }
}
