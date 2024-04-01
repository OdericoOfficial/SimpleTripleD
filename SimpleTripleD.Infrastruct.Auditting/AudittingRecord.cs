using System.Text.Json;

namespace SimpleTripleD.Infrastruct.Auditting
{
    internal class AudittingRecord
    {
        public long Id { get; set; }

        public string? TableName { get; private set; }

        public string? OriginalValues { get; private set; }

        public string? NewValues { get; private set; }

        public string? EntityState { get; private set; }

        public DateTime CreateTime { get; private set; }

        public string? CreatorId { get; private set; }

        public DateTime LastModifiedTime { get; private set; }

        public string? LastModifierId { get; private set; }

        public bool IsDelete { get; private set; }

        public DateTime DeleteTime { get; private set; }

        public string? DeleterId { get; private set; }

        public string? PrimaryKey { get; private set; }
    
        public AudittingRecord(AudittingEntry entry)
        {
            TableName = entry.TableName;
            OriginalValues = JsonSerializer.Serialize(entry.OriginalValues);
            NewValues = JsonSerializer.Serialize(entry.NewValues);
            DeleterId = entry.DeleterId;
            PrimaryKey = JsonSerializer.Serialize(entry.PrimaryKey);
            EntityState = JsonSerializer.Serialize(entry.EntityState);
            CreateTime = entry.CreateTime;
            LastModifiedTime = entry.LastModifiedTime;
            CreatorId = entry.CreatorId;
            LastModifierId = entry.LastModifierId;
            IsDelete = entry.IsDelete;
            DeleteTime = entry.DeleteTime;
        }
    }
}
