using SimpleTripleD.Domain.MultiTenancy;

namespace SimpleTripleD.Domain.Auditting
{
    public class AudittingRecord : MultiTenancyAggregateRoot<Guid>
    {
        private IEnumerable<EntityChangeRecord>? _entityChangedRecords;
        public IEnumerable<EntityChangeRecord>? EntityChangeRecords
            => _entityChangedRecords;

        private ExecutingMethodRecord _executingMethodRecord;
        public ExecutingMethodRecord ExecutingMethodRecord
            => _executingMethodRecord;

        public AudittingRecord(IEnumerable<EntityChangeRecord>? entityChangeRecords, ExecutingMethodRecord executingMethodRecord)
        {
            _entityChangedRecords = entityChangeRecords;
            _executingMethodRecord = executingMethodRecord;
        }
    }
}
