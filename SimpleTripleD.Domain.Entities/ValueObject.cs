namespace SimpleTripleD.Domain.Entities
{
    public abstract class ValueObject<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {
        public override bool Equals(object? obj)
        {
            var valueObject = obj as TValueObject;
            return !ReferenceEquals(valueObject, null) && EqualsInternal(valueObject);
        }

        protected abstract bool EqualsInternal(TValueObject obj);

        public override int GetHashCode()
            => GetHashCodeInternal();

        protected abstract int GetHashCodeInternal();
    }
}
