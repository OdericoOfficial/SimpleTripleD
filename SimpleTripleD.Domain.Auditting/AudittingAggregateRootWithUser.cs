using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Auditting
{
    public abstract class AudittingAggregateRootWithUser<TUser> : AudittingAggregateRoot, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents, IAudittingObject<TUser>, IAudittingObject
    {
        public virtual TUser? Creator { get; }

        public virtual TUser? LastModifier { get; }

        public virtual TUser? Deleter { get; }
    }

    public abstract class AudittingAggregateRootWithUser<TKey, TUser> : AudittingAggregateRoot<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents, IAudittingObject<TUser>, IAudittingObject
    {
        public virtual TUser? Creator { get; }

        public virtual TUser? LastModifier { get; }

        public virtual TUser? Deleter { get; }

        public AudittingAggregateRootWithUser()
        {
        }
    }
}
