using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;

namespace SimpleTripleD.Domain.Entities.Auditting
{
    public abstract class AudittingAggregateRootWithUser<TUser> : AudittingAggregateRoot, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject<TUser>, IAudittingObject
    {
        public virtual TUser? Creator { get; }

        public virtual TUser? LastModifier { get; }

        public virtual TUser? Deleter { get; }
    }

    public abstract class AudittingAggregateRootWithUser<TKey, TUser> : AudittingAggregateRoot<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject<TUser>, IAudittingObject
    {
        public virtual TUser? Creator { get; }

        public virtual TUser? LastModifier { get; }

        public virtual TUser? Deleter { get; }

        public AudittingAggregateRootWithUser(TKey id) : base(id)
        {
        }
    }
}
