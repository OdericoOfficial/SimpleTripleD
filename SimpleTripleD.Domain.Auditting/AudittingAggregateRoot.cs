using SimpleTripleD.Domain.Events.Distributed;
using SimpleTripleD.Domain.Events;
using SimpleTripleD.Domain.MultiTenancy;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Auditting
{
    public abstract class AudittingAggregateRoot : MultiTenancyAggregateRoot, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents, IAudittingObject
    {
        public DateTime CreateTime { get; set; }
        
        public string? CreatorId { get; set; }
        
        public DateTime LastModifiedTime { get; set; }
        
        public string? LastModifierId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DeleteTime { get; set; }
        
        public string? DeleterId { get; set; }
    }

    public abstract class AudittingAggregateRoot<TKey> : MultiTenancyAggregateRoot<TKey>, IAggregateRoot<TKey>, IAggregateRoot, IEntity, IDomainEvents, IIntegrationEvents, IAudittingObject
    {
        public DateTime CreateTime { get; set; }

        public string? CreatorId { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string? LastModifierId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DeleteTime { get; set; }

        public string? DeleterId { get; set; }
    }
}
