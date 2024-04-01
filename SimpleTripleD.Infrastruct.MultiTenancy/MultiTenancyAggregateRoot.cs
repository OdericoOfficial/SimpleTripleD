using SimpleTripleD.Domain.Entities.Auditting;
using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;

namespace SimpleTripleD.Domain.Entities.MultiTenancy
{
    public abstract class MultiTenancyAggregateRoot : AudittingAggregateRoot, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject, IMultiTenancyObject
    {
        public Guid TenantId { get; set; }
    }

    public abstract class MultiTenancyAggregateRoot<TKey> : AudittingAggregateRoot<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject, IMultiTenancyObject
    {
        public Guid TenantId { get; set; }

        public MultiTenancyAggregateRoot(TKey id) : base(id)
        {
        }
    }
}
