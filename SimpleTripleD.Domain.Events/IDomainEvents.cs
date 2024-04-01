namespace SimpleTripleD.Domain.Events
{
    public interface IDomainEvents
    {
        IEnumerable<DomainEventArgs> DomainEvents { get; }
        void AddDomainEvent<TDomainEventArgs>(TDomainEventArgs args) where TDomainEventArgs : DomainEventArgs;
        void ClearDomainEvents();
    }
}