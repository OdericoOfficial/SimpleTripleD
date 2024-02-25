using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Events.Distributed;
using System.Data;

namespace SimpleTripleD.Domain.Repositories
{
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly TDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public IDbContextTransaction? CurrentTransaction
            => _currentTransaction;

        public bool HasActiveTransaction
            => _currentTransaction is not null;

        public UnitOfWork(IMediator mediator, TDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync(cancellationToken);
            return result;
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var localDomainEvents = _context.ChangeTracker.Entries<ILocalDomainEvents>()
                .Where(item => item.Entity.LocalEvents.Any());

            var distributedDoaminEvents = _context.ChangeTracker.Entries<IDistributedDomainEvents>()
                .Where(item => item.Entity.DistributedEvents.Any());

            foreach (var localDomainEvent in localDomainEvents.SelectMany(item => item.Entity.LocalEvents))
                await _mediator.Publish(localDomainEvent, cancellationToken);

            foreach (var distributedDoaminEvent in distributedDoaminEvents.SelectMany(item => item.Entity.DistributedEvents))
                await _mediator.Publish(distributedDoaminEvent, cancellationToken);

            foreach (var localDomainEvent in localDomainEvents)
                localDomainEvent.Entity.ClearLocalEvents();

            foreach (var distributedDomainEvent in distributedDoaminEvents)
                distributedDomainEvent.Entity.ClearDistributedEvents();
        }

        public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is not null) 
                return null;
            return _currentTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction != _currentTransaction) 
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            
            try
            {
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        private async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction is not null)
                    await _currentTransaction.RollbackAsync(cancellationToken);
            }
            finally 
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public void Dispose()
            => _context.Dispose();

        public ValueTask DisposeAsync()
            => _context.DisposeAsync();
    }
}