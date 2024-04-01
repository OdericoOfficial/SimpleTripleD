namespace SimpleTripleD.Domain.Entities
{
#nullable disable
    [Serializable]
    public abstract class Entity : IEntity
    {
        public abstract object[] Keys { get; }
    }

    [Serializable]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>, IEntity
    {
        private TKey _id;
        public TKey Id
            => _id;

        public Entity() { }

        public override object[] Keys
            => IsTransient() ? [] : [Id];

        public bool IsTransient()
            => EqualityComparer<TKey>.Default.Equals(_id, default(TKey));
    }
}