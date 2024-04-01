using SimpleTripleD.Domain.Events;

namespace SimpleTripleD.Domain.Cache
{
    public class TryDeleteCacheEventArgs<TKey> : DomainEventArgs
    {
        public TKey AggregateRootId { get; }

        public string StoreName { get; }

        public TryDeleteCacheEventArgs(TKey aggregateRootId, string storeName) : base()
        {
            AggregateRootId = aggregateRootId;
            StoreName = storeName;
        }        
    }
}