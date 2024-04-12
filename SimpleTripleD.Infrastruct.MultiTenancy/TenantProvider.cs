using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleTripleD.Domain.MultiTenancy;
using SimpleTripleD.Infrastruct.Repositories;

namespace SimpleTripleD.Infrastruct.MultiTenancy
{
    public class TenantProvider : ITenantProvider
    {
        private readonly string? _host;
        private readonly IReadOnlyRepository<Tenant> _repository;
        private Guid? _tenantId;
        private string? _connectString;

        public TenantProvider(IHttpContextAccessor accessor, IReadOnlyRepository<Tenant> repository) 
        {
            _host = accessor.HttpContext?.Request.Host.Value;
            _repository = repository;
        }

        public string? GetConnnectString()
        {
            if (_connectString is not null)
                return _connectString;

            if (_host is null)
                return null;

            _connectString = _repository.AsQueryable().FirstOrDefault(item => item.Host == _host)?.ConnectionString;
            return _connectString;
        }

        public async Task<string?> GetConnnectStringAsync()
        {
            if (_connectString is not null)
                return _connectString;

            if (_host is null)
                return null;

            _connectString = (await _repository.FirstOrDefaultAsync(item => item.Host == _host).ConfigureAwait(false))?.ConnectionString;
            return _connectString;
        }

        public Guid GetTenantId()
        {
            if (_tenantId.HasValue)
                return _tenantId.Value;

            if (_host is null)
                return Guid.Empty;

            _tenantId = _repository.AsQueryable().FirstOrDefault(item => item.Host == _host)?.Id ?? Guid.Empty;
            return _tenantId.Value;
        }

        public async Task<Guid> GetTenantIdAsync()
        {
            if (_tenantId.HasValue)
                return _tenantId.Value;

            if (_host is null)
                return Guid.Empty;

            _tenantId = (await _repository.FirstOrDefaultAsync(item => item.Host == _host).ConfigureAwait(false))?.Id ?? Guid.Empty;
            return _tenantId.Value;
        }
    }
}
