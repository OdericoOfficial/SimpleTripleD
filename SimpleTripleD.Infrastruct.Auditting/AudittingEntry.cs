using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTripleD.Domain.Entities.Auditting;

namespace SimpleTripleD.Infrastruct.Auditting
{
    internal class AudittingEntry
    {
        public string? TableName { get; }

        public Dictionary<string, object?>? OriginalValues { get; }

        public Dictionary<string, object?>? NewValues { get; }

        public EntityState EntityState { get; }

        public DateTime CreateTime { get; }
        
        public string? CreatorId { get; }
        
        public DateTime LastModifiedTime { get; }
        
        public string? LastModifierId { get; }
        
        public bool IsDelete { get; }
        
        public DateTime DeleteTime { get; }
        
        public string? DeleterId { get; }

        public object? PrimaryKey { get; }

        public AudittingEntry(EntityEntry entry, IAudittingObject obj)
        {
            TableName = entry.Metadata.GetTableName() ?? entry.Metadata.Name;

            CreateTime = obj.CreateTime;
            CreatorId = obj.CreatorId;
            LastModifiedTime = obj.LastModifiedTime;
            LastModifierId = obj.LastModifierId;
            IsDelete = obj.IsDelete;
            DeleteTime = obj.DeleteTime;
            DeleterId = obj.DeleterId;

            switch (entry.State)
            {
                case EntityState.Added:
                    NewValues = new Dictionary<string, object?>();
                    break;
                case EntityState.Modified:
                    OriginalValues = new Dictionary<string, object?>();
                    NewValues = new Dictionary<string, object?>();
                    break;
                case EntityState.Deleted:
                    OriginalValues = new Dictionary<string, object?>();
                    break;
            }
        
            foreach (var item in entry.Properties)
            {
                var column = item.Metadata.GetColumnName();

                if (item.Metadata.IsPrimaryKey())
                    PrimaryKey = item.CurrentValue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        NewValues![column] = item.CurrentValue;
                        break;
                    case EntityState.Modified:
                        NewValues![column] = item.CurrentValue;
                        OriginalValues![column] = item.OriginalValue;
                        break;
                    case EntityState.Deleted:
                        OriginalValues![column] = item.OriginalValue;
                        break;
                }
            }
        }
    }
}
