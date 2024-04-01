namespace SimpleTripleD.Infrastruct.Dapr
{
    public class DaprOptions
    {
        public Dictionary<string, string>? EndPoints { get; set; }
        public string? SecretStore { get; set; }
        public string? StateStore { get; set; }
        public string? PubSub { get; set; }
    }
}
