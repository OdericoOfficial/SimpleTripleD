namespace SimpleTripleD.Modules.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class LifeCircleAttribute : Attribute
    {
        public LifeCircle LifeCircle { get; }

        public Type? ServiceType { get; }

        public string? ServiceKey { get; }

        public LifeCircleAttribute(LifeCircle lifeCircle, Type? serviceType = null, string? serviceKey = null)
        {
            LifeCircle = lifeCircle;
            ServiceType = serviceType;
            ServiceKey = serviceKey;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class LifeCircleAttribute<TService> : LifeCircleAttribute
    {
        public LifeCircleAttribute(LifeCircle lifeCircle, string? serviceKey = null) : base(lifeCircle, typeof(TService), serviceKey)
        {
        }
    }
}
