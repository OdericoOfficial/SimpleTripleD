namespace SimpleTripleD.Domain.Entities
{
    public interface IEntity
    {
        object[] Keys { get; }
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}