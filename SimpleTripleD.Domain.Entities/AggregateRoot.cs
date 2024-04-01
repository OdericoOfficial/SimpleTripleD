using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Events.Distributed;
using System.Collections.ObjectModel;

namespace SimpleTripleD.Domain.Entities
{
    [Serializable]
    public abstract class AggregateRoot : Entity, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents
    {
        private readonly ICollection<DomainEventArgs> _domainEvents
            = new Collection<DomainEventArgs>();

        private readonly ICollection<IntegrationEventArgs> _integrationEvents
            = new Collection<IntegrationEventArgs>();

        public IEnumerable<DomainEventArgs> DomainEvents
            => _domainEvents;
        
        public IEnumerable<IntegrationEventArgs> IntegrationEvents
            => _integrationEvents;

        public virtual void ClearDomainEvents()
            => _domainEvents.Clear();

        public virtual void ClearIntegrationEvents()
            => _integrationEvents.Clear();

        public void AddDomainEvent<TDomainEventArgs>(TDomainEventArgs args) where TDomainEventArgs : DomainEventArgs
            => _domainEvents.Add(args);

        public void AddIntegrationEvent<TIntegrationEventArgs>(TIntegrationEventArgs args) where TIntegrationEventArgs : IntegrationEventArgs
            => _integrationEvents.Add(args);
    }

    [Serializable]
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents
    {

        private readonly ICollection<DomainEventArgs> _domainEvents
            = new Collection<DomainEventArgs>();

        private readonly ICollection<IntegrationEventArgs> _integrationEvents
            = new Collection<IntegrationEventArgs>();

        public IEnumerable<DomainEventArgs> DomainEvents
            => _domainEvents;

        public IEnumerable<IntegrationEventArgs> IntegrationEvents
            => _integrationEvents;

        public virtual void ClearDomainEvents()
            => _domainEvents.Clear();

        public virtual void ClearIntegrationEvents()
            => _integrationEvents.Clear();

        public void AddDomainEvent<TDomainEventArgs>(TDomainEventArgs args) where TDomainEventArgs : DomainEventArgs
            => _domainEvents.Add(args);

        public void AddIntegrationEvent<TIntegrationEventArgs>(TIntegrationEventArgs args) where TIntegrationEventArgs : IntegrationEventArgs
            => _integrationEvents.Add(args);
    
        public AggregateRoot()
        {
        }
    }
}