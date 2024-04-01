namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public interface ITenantProvider
    {
        Guid GetTenantId();
        
        Task<Guid> GetTenantIdAsync();

        string? GetConnnectString();

        Task<string?> GetConnnectStringAsync();
    }
}
