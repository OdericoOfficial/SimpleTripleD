using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.MultiTenancy
{
    public class Tenant : AggregateRoot<Guid>
    {
        private string _name;
        public string Name
            => _name;

        private string _host;
        public string Host 
            => _host;

        private string _connectionString;
        public string ConnectionString
            => _connectionString;

        public Tenant(string name, string host, string connectionString)
        {
            _name = name;
            _host = host;
            _connectionString = connectionString;
        }
    }
}
