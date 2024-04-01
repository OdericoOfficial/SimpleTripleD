using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Domain.Auditting
{
    public class EntityChangeRecord : ValueObject<EntityChangeRecord>
    {
        private string? _aggregateRootId;
        public string? AggregateRootId
            => _aggregateRootId;

        private Guid _aggregateRootTenantId;
        public Guid AggregateRootTenantId
            => _aggregateRootTenantId;

        private string? _originValues;
        public string? OriginValues
            => _originValues;

        private string? _changedValues;
        public string? ChangedValues
            => _changedValues;

        private string? _entityState;
        public string? EntityState
            => _entityState;

        private DateTime _createTime;
        public DateTime CreateTime
            => _createTime;

        private string? _creatorId;
        public string? CreatorId
            => _creatorId;

        private DateTime _lastModifiedTime;
        public DateTime LastModifiedTime
            => _lastModifiedTime;

        private string? _lastModifierId;
        public string? LastModifierId
            => _lastModifierId;

        private bool _isDelete;
        public bool IsDelete
            => _isDelete;

        private DateTime _deleteTime;
        public DateTime DeleteTime
            => _deleteTime;

        private string? _deleterId;
        public string? DeleterId
            => _deleterId;

        public EntityChangeRecord(string? aggregateRootId, Guid aggregateRootTenantId, string? originValues,
            string? changedValues, string? entityState, DateTime createTime, string? creatorId, DateTime lastModifiedTime,
                string? lastModifierId, bool isDelete, DateTime deleteTime, string? deleterId)
        {
            _aggregateRootId = aggregateRootId;
            _aggregateRootTenantId = aggregateRootTenantId;
            _originValues = originValues;
            _changedValues = changedValues;
            _entityState = entityState;
            _createTime = createTime;
            _creatorId = creatorId;
            _lastModifiedTime = lastModifiedTime;
            _lastModifierId = lastModifierId;
            _isDelete = isDelete;
            _deleteTime = deleteTime;
            _deleterId = deleterId;
        }

        protected override bool EqualsInternal(EntityChangeRecord obj)
            => ReferenceEquals(obj, this) || (obj._aggregateRootId == _aggregateRootId
                && obj._aggregateRootTenantId == _aggregateRootTenantId
                && obj._entityState == _entityState
                && obj._lastModifiedTime == _lastModifiedTime
                && obj._deleteTime == _deleteTime);

        protected override int GetHashCodeInternal()
            => _aggregateRootId?.GetHashCode() ?? 0
                ^ _aggregateRootTenantId.GetHashCode()
                ^ _entityState?.GetHashCode() ?? 0
                ^ _lastModifiedTime.GetHashCode()
                ^ _deleteTime.GetHashCode();
    }
}
