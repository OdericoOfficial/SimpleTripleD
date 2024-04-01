using Microsoft.EntityFrameworkCore.Storage;

namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IDbContextTransaction? CurrentTransaction { get; }
        bool HasActiveTransaction { get; }
        Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}