namespace SimpleTripleD.Infrastruct.Dapr
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DaprInvocationAttribute : Attribute
    {
        public string ClusterName { get; }

        public string ControllerName { get; }

        public DaprInvocationAttribute(string clusterName, string controllerName)
        {
            ClusterName = clusterName;
            ControllerName = controllerName;
        }
    }
}
