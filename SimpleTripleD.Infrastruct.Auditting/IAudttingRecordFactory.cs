using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTripleD.Domain.Auditting;

namespace SimpleTripleD.Infrastruct.Auditting
{
    public interface IAudttingRecordFactory
    {
        void AddEntityRecords(IEnumerable<EntityEntry> entities);

        void SetParameterInfo(string parameter);

        AudittingRecord Create(string? clientIpAddress, string? browserInfo, string? httpMethod, int? httpStatusCode,
            string? url, DateTime executionTime, long executionDuration, string? exception);
    }
}
