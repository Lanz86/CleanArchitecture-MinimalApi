namespace CleanArchitecture.Domain.Common;

public interface IBaseAuditableEntry
{
    DateTime Created { get; set; }

    string? CreatedBy { get; set; }

    DateTime? LastModified { get; set; }

    string? LastModifiedBy { get; set; }
}

public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IBaseAuditableEntry where TKey : struct
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
