using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Infrastruct.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.TryAddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();
            return services;
        }

        public static IServiceCollection AddRepository<TDbContext, TAggregateRoot>(this IServiceCollection services)
            where TDbContext : DbContext
            where TAggregateRoot : class, IAggregateRoot
        {
            services.TryAddScoped<IUnitOfWorkAccessor<TAggregateRoot>, Repository<TDbContext, TAggregateRoot>>();
            services.TryAddScoped<IReadOnlyRepository<TAggregateRoot>, Repository<TDbContext, TAggregateRoot>>();
            services.TryAddScoped<IRepository<TAggregateRoot>, Repository<TDbContext, TAggregateRoot>>();
            return services;
        }

        public static IServiceCollection AddRepository<TDbContext, TAggregateRoot, TKey>(this IServiceCollection services)
            where TDbContext : DbContext
            where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            services.TryAddScoped<IUnitOfWorkAccessor<TAggregateRoot>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            services.TryAddScoped<IReadOnlyRepository<TAggregateRoot>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            services.TryAddScoped<IRepository<TAggregateRoot>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            services.TryAddScoped<IRepositoryWithCache<TAggregateRoot, TKey>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            services.TryAddScoped<IReadOnlyRepositoryWithCache<TAggregateRoot, TKey>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            services.TryAddScoped<IRepository<TAggregateRoot, TKey>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            services.TryAddScoped<IReadOnlyRepository<TAggregateRoot, TKey>, RepositoryWithCache<TDbContext, TAggregateRoot, TKey>>();
            return services;
        }
    }
}
