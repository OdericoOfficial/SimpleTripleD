using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;

namespace SimpleTripleD.Domain.Entities.Auditting
{
    public abstract class AudittingAggregateRoot : AggregateRoot, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject
    {
        public DateTime CreateTime { get; set; }

        public string? CreatorId { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string? LastModifierId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DeleteTime { get; set; }

        public string? DeleterId { get; set; }
    }

    public abstract class AudittingAggregateRoot<TKey> : AggregateRoot<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, ILocalDomainEvents, IDistributedDomainEvents, IAudittingObject
    {
        public DateTime CreateTime { get; set; }

        public string? CreatorId { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string? LastModifierId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DeleteTime { get; set; }

        public string? DeleterId { get; set; }

        public AudittingAggregateRoot(TKey id) : base(id)
        {
        }
    }
}
