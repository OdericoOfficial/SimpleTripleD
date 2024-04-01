using SimpleTripleD.Domain.Entities.Auditting;
using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;

namespace SimpleTripleD.Domain.Entities.MultiTenancy
{
    public abstract class MultiTenancyAggregateRootWithUser<TUser> : AudittingAggregateRootWithUser<TUser>, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject<TUser>, IAudittingObject, IMultiTenancyObject
    {
        public Guid TenantId { get; set; }
    }

    public abstract class MultiTenancyAggregateRootWithUser<TKey, TUser> : AudittingAggregateRootWithUser<TKey, TUser>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject<TUser>, IAudittingObject, IMultiTenancyObject
    {
        public Guid TenantId { get; set; }

        public MultiTenancyAggregateRootWithUser(TKey id) : base(id)
        {
        }
    }
}
