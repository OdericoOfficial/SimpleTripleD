using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Events.Distributed;
using System.Collections.ObjectModel;

namespace SimpleTripleD.Domain.Entities
{
    [Serializable]
    public abstract class AggregateRoot : Entity, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents
    {
        private readonly ICollection<LocalDomainEventArgs> _localEvents
            = new Collection<LocalDomainEventArgs>();

        private readonly ICollection<DistributedDomainEventArgs> _distributedEvents
            = new Collection<DistributedDomainEventArgs>();

        public IEnumerable<LocalDomainEventArgs> LocalEvents
            => _localEvents;
        
        public IEnumerable<DistributedDomainEventArgs> DistributedEvents 
            => _distributedEvents;

        public virtual void ClearLocalEvents()
            => _distributedEvents.Clear();

        public virtual void ClearDistributedEvents()
            => _localEvents.Clear();

        public void AddLocalEvent(IntegrationEvent integrationEvent)
            => _localEvents.Add(new LocalDomainEventArgs(integrationEvent));

        public void AddDistributedEvent(IntegrationEvent integrationEvent, string pubSub, string topic)
            => _distributedEvents.Add(new DistributedDomainEventArgs(integrationEvent, pubSub, topic));
    }

    [Serializable]
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents
    {
        private readonly ICollection<LocalDomainEventArgs> _localEvents
            = new Collection<LocalDomainEventArgs>();

        private readonly ICollection<DistributedDomainEventArgs> _distributedEvents
            = new Collection<DistributedDomainEventArgs>();

        public IEnumerable<LocalDomainEventArgs> LocalEvents
            => _localEvents;

        public IEnumerable<DistributedDomainEventArgs> DistributedEvents
            => _distributedEvents;

        public AggregateRoot(TKey id) : base(id)
        {
        }

        public virtual void ClearLocalEvents()
            => _distributedEvents.Clear();

        public virtual void ClearDistributedEvents()
            => _localEvents.Clear();

        public void AddLocalEvent(IntegrationEvent integrationEvent)
            => _localEvents.Add(new LocalDomainEventArgs(integrationEvent));

        public void AddDistributedEvent(IntegrationEvent integrationEvent, string pubSub, string topic)
            => _distributedEvents.Add(new DistributedDomainEventArgs(integrationEvent, pubSub, topic));
    }
}