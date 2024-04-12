using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Events.Distributed;
using System.Data;

namespace SimpleTripleD.Infrastruct.Repositories
{
    internal class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>, IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly TDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public IDbContextTransaction? CurrentTransaction
            => _currentTransaction;

        public bool HasActiveTransaction
            => _currentTransaction is not null;

        public DbContext Context 
            => _context;

        TDbContext IUnitOfWork<TDbContext>.Context 
            => _context;

        public UnitOfWork(IMediator mediator, TDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            (var domainEventArgs, var integrationEventArgs) = SelectEventArgs();
            var result = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await DispatchDomainEventsAsync(domainEventArgs, integrationEventArgs, cancellationToken).ConfigureAwait(false);
            return result;
        }

        private (IEnumerable<EntityEntry<IDomainEvents>> domainEventArgs, IEnumerable<EntityEntry<IIntegrationEvents>> integrationEventArgs) SelectEventArgs()
            => (_context.ChangeTracker.Entries<IDomainEvents>().Where(item => item.Entity.DomainEvents.Any()).ToArray(),
                _context.ChangeTracker.Entries<IIntegrationEvents>().Where(item => item.Entity.IntegrationEvents.Any()).ToArray());

        private async Task DispatchDomainEventsAsync(IEnumerable<EntityEntry<IDomainEvents>> domainEventArgs,
            IEnumerable<EntityEntry<IIntegrationEvents>> integrationEventArgs, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEventArgs.SelectMany(item => item.Entity.DomainEvents))
                await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);

            foreach (var integrationEvent in integrationEventArgs.SelectMany(item => item.Entity.IntegrationEvents))
                await _mediator.Publish(integrationEvent, cancellationToken).ConfigureAwait(false);

            foreach (var item in domainEventArgs)
                item.Entity.ClearDomainEvents();

            foreach (var item in integrationEventArgs)
                item.Entity.ClearIntegrationEvents();
        }

        public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction is not null) 
                return null;
            return _currentTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction != _currentTransaction) 
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            
            try
            {
                await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync().ConfigureAwait(false);
                    _currentTransaction = null;
                }
            }
        }

        private async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction is not null)
                    await _currentTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            }
            finally 
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync().ConfigureAwait(false);
                    _currentTransaction = null;
                }
            }
        }

        public void Dispose()
        {
            if (_currentTransaction is not null)
            {
                _currentTransaction.Rollback();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.RollbackAsync().ConfigureAwait(false);
                await _currentTransaction.DisposeAsync().ConfigureAwait(false);
                _currentTransaction = null;
            }
            await _context.DisposeAsync().ConfigureAwait(false);
        }
    }
}