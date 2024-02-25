namespace SimpleTripleD.Domain.Entities
{
    [Serializable]
    public abstract class Entity : IEntity
    {
        public abstract object?[] GetKeys();
    }

    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey>, IEntity
    {
        public TKey Id { get; }

        public Entity(TKey id)
            => Id = id;

        public object?[] GetKeys()
            => new object?[] { Id };
    }
}