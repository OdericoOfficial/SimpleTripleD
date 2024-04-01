using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Events.Distributed;

namespace SimpleTripleD.Domain.Entities
{
    public interface IAggregateRoot : IEntity, IDomainEvents, IIntegrationEvents
    {
    }

    public interface IAggregateRoot<TKey> : IAggregateRoot, IEntity<TKey>, IEntity, IDomainEvents, IIntegrationEvents
    {
    }
}