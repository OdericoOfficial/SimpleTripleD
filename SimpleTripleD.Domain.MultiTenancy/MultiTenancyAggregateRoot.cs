using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.MultiTenancy
{
    public abstract class MultiTenancyAggregateRoot : AggregateRoot, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents, IMultiTenancyObject
    {
        public Guid TenantId { get; set; }
    }

    public abstract class MultiTenancyAggregateRoot<TKey> : AggregateRoot<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents, IMultiTenancyObject
    {
        public Guid TenantId { get; set; }
    }
}
