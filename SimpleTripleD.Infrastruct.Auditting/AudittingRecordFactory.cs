using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Domain.Entities;
using SimpleTripleD.Domain.MultiTenancy;
using System.Text.Json;

namespace SimpleTripleD.Infrastruct.Auditting
{
    internal class AudittingRecordFactory : IAudttingRecordFactory
    {
        private IEnumerable<EntityChangeRecord>? _entityChangeRecords;
        private string? _parameter;

        public void AddEntityRecords(IEnumerable<EntityEntry> entities)
            => _entityChangeRecords = entities.Select(item =>
            {
                var query = item.Properties.Where(item => item.IsModified);
                var originalValues = JsonSerializer.Serialize(query.Select(item => item.OriginalValue));
                var currentValues = JsonSerializer.Serialize(query.Select(item => item.CurrentValue));
                var keys = JsonSerializer.Serialize(((IEntity)item.Entity).Keys);
                var audtting = ((IAudittingObject)item.Entity);
                return new EntityChangeRecord(keys, ((IMultiTenancyObject)item.Entity).TenantId, originalValues,
                    currentValues, item.State.ToString(), audtting.CreateTime, audtting.CreatorId, audtting.LastModifiedTime, audtting.LastModifierId,
                        audtting.IsDelete, audtting.DeleteTime, audtting.DeleterId);
            }).ToList();

        public void SetParameterInfo(string parameter)
            => _parameter = parameter;

        public AudittingRecord Create(string? clientIpAddress, string? browserInfo, string? httpMethod, int? httpStatusCode,
            string? url, DateTime executionTime, long executionDuration, string? exception)
            => new AudittingRecord(_entityChangeRecords, new ExecutingMethodRecord(clientIpAddress, browserInfo, httpMethod,
                httpStatusCode, url, _parameter, executionTime, executionDuration, exception));
    }
}
