using MediatR;

namespace SimpleTripleD.Domain.Repositories
{
    public class TryDeleteCacheEventArgs<TKey> : INotification
    {
        public TKey Id { get; }

        public string StoreName { get; }

        public TryDeleteCacheEventArgs(TKey id, string storeName)
        {
            Id = id; 
            StoreName = storeName;
        }
    }
}