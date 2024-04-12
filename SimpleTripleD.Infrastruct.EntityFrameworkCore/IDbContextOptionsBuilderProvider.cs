using Microsoft.EntityFrameworkCore;

namespace SimpleTripleD.Infrastruct.EntityFrameworkCore
{
    public interface IDbContextOptionsBuilderProvider
    {
        ValueTask ProvideDbContextOptionsBuilderAction(IServiceProvider provider, DbContextOptionsBuilder builder);
    }
}
