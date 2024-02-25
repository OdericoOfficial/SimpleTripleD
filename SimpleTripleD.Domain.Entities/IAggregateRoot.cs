using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Events.Distributed;

namespace SimpleTripleD.Domain.Entities
{
    public interface IAggregateRoot : IEntity, ILocalDomainEvents, IDistributedDomainEvents
    {
    }

    public interface IAggregateRoot<TKey> : IAggregateRoot, IEntity<TKey>, IEntity, ILocalDomainEvents, IDistributedDomainEvents
    {
    }
}