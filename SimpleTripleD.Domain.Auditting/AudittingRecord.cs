using SimpleTripleD.Domain.MultiTenancy;

namespace SimpleTripleD.Domain.Auditting
{
    public class AudittingRecord : MultiTenancyAggregateRoot<Guid>
    {
        private IReadOnlyCollection<EntityChangeRecord> _entityChangedRecords;
        public IReadOnlyCollection<EntityChangeRecord> EntityChangeRecords
            => _entityChangedRecords;

        private ExecutingMethodRecord _executingMethodRecord;
        public ExecutingMethodRecord ExecutingMethodRecord
            => _executingMethodRecord;

        public AudittingRecord(IReadOnlyCollection<EntityChangeRecord> entityChangeRecords, ExecutingMethodRecord executingMethodRecord)
        {
            _entityChangedRecords = entityChangeRecords;
            _executingMethodRecord = executingMethodRecord;
        }
    }
}
