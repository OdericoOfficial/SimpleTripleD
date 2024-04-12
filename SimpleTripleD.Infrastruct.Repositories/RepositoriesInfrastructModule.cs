using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Infrastruct.Dapr;
using SimpleTripleD.Modules;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public class RepositoriesInfrastructModule<TDbContext, TAggregateRoot> : DependencyModule
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddUnitOfWork<TDbContext>();
            builder.Services.AddRepository<TDbContext, TAggregateRoot>();
        }
    }

    [DependencyModules(typeof(DaprInfrastructModule))]
    public class RepositoriesInfrastructModule<TDbContext, TAggregateRoot, TKey> : DependencyModule
        where TDbContext : DbContext
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddUnitOfWork<TDbContext>();
            builder.Services.AddRepository<TDbContext, TAggregateRoot, TKey>();
        }
    }
}
