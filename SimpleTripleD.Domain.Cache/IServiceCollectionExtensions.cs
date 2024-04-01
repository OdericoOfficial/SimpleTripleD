using MediatR;
using SimpleTripleD.Domain.Cache;
using SimpleTripleD.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTryDeleteCacheEventHandler<TAggregateRoot, TKey>(this IServiceCollection services)
            where TAggregateRoot : IAggregateRoot<TKey>
            => services.AddScoped<INotificationHandler<TryDeleteCacheEventArgs<TKey>>, TryDeleteCacheEventHandler<TAggregateRoot, TKey>>();
    }
}
