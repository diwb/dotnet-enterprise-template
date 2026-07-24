namespace Domain.Common;

public abstract class AuditableEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAtUtc { get; private set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; private set; } = "system";
    public DateTimeOffset? UpdatedAtUtc { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTimeOffset? DeletedAtUtc { get; private set; }
    public string? DeletedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    public void MarkCreated(string user, DateTimeOffset now)
    {
        CreatedBy = string.IsNullOrWhiteSpace(user) ? "system" : user;
        CreatedAtUtc = now;
    }

    public void MarkUpdated(string user, DateTimeOffset now)
    {
        UpdatedBy = string.IsNullOrWhiteSpace(user) ? "system" : user;
        UpdatedAtUtc = now;
    }

    public void SoftDelete(string user, DateTimeOffset now)
    {
        IsDeleted = true;
        DeletedBy = string.IsNullOrWhiteSpace(user) ? "system" : user;
        DeletedAtUtc = now;
    }
}
