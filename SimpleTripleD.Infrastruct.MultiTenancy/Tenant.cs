namespace SimpleTripleD.Domain.Entities.MultiTenancy
{
#nullable disable
    public class Tenant 
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Host {  get; set; }   
        
        public string ConnectionString { get; set; }
    }
}
