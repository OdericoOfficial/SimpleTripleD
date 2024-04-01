using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Auditting
{
    public class ExecutingMethodRecord : ValueObject<ExecutingMethodRecord>
    {
        private string? _clientIpAddress;
        public string? ClientIpAddress
            => _clientIpAddress;

        private string? _browserInfo;
        public string? BrowserInfo
            => _browserInfo;    

        private string? _httpMethod;
        public string? HttpMethod
            => _httpMethod;

        private int? _httpStatusCode;
        public int? HttpStatusCode
            => _httpStatusCode;

        private string? _url;
        public string? Url
            => _url;

        private DateTime _executionTime;
        public DateTime ExecutionTime
            => _executionTime;

        private int _executionDuration;
        public int ExecutionDuration
            => _executionDuration;

        private string? _exception;
        public string? Exception
            => _exception;

        public ExecutingMethodRecord(string? clientIpAddress, string? browserInfo, string? httpMethod, int? httpStatusCode,
            string? url, DateTime executionTime, int executionDuration, string? exception)
        {
            _clientIpAddress = clientIpAddress;
            _browserInfo = browserInfo;
            _httpMethod = httpMethod;
            _httpStatusCode = httpStatusCode;
            _url = url;
            _executionTime = executionTime;
            _executionDuration = executionDuration;
            _exception = exception;
        }

        protected override bool EqualsInternal(ExecutingMethodRecord obj)
            => ReferenceEquals(obj, this) || (obj._clientIpAddress == _clientIpAddress
                && obj._browserInfo == _browserInfo
                && obj._httpMethod == _httpMethod
                && obj._httpStatusCode == _httpStatusCode
                && obj._url == _url
                && obj._executionTime == _executionTime
                && obj._executionDuration == _executionDuration);

        protected override int GetHashCodeInternal()
            => _clientIpAddress?.GetHashCode() ?? 0
                ^ _browserInfo?.GetHashCode() ?? 0
                ^ _httpMethod?.GetHashCode() ?? 0
                ^ _httpStatusCode ?? 0 
                ^ _url?.GetHashCode() ?? 0 
                ^ _executionTime.GetHashCode()
                ^ _executionDuration;
    }
}
