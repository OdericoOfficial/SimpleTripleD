namespace SimpleTripleD.Infrastruct.Dapr
{
    public class DaprOptions
    {
        public Dictionary<string, Dictionary<string, string>>? Routes { get; set; }

        public string? SecretStore { get; set; }
        
        public string? StateStore { get; set; }
    }
}
