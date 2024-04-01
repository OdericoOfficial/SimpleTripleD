namespace SimpleTripleD.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DependencyModulesAttribute : Attribute
    {
        public IEnumerable<Type> ModuleTypes { get; }

        public DependencyModulesAttribute(params Type[] moduleTypes) 
            => ModuleTypes = moduleTypes.Where(item => typeof(DependencyModule).IsAssignableFrom(item));
    }
}
