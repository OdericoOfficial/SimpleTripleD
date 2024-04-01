namespace SimpleTripleD.Domain.Entities.MultiTenancy
{
    public interface IMultiTenancyObject
    {
        Guid TenantId { get; set; }
    }
}
