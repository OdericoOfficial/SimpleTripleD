namespace SimpleTripleD.Domain.MultiTenancy
{
    public interface IMultiTenancyObject
    {
        Guid TenantId { get; set; }
    }
}
