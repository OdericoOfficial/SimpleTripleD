namespace SimpleTripleD.Domain.Entities.Auditting
{
    public interface IAudittingObject
    {
        DateTime CreateTime { get; set; }
        string? CreatorId { get; set; }
        DateTime LastModifiedTime { get; set; }
        string? LastModifierId { get; set; }
        bool IsDelete { get; set; }
        DateTime DeleteTime { get; set; }
        string? DeleterId { get; set; }
    }

    public interface IAudittingObject<TUser> : IAudittingObject 
    {
        TUser? Creator { get; }
        TUser? LastModifier { get; }
        TUser? Deleter { get; }
    }
}
