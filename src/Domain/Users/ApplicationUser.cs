using Domain.Common;

namespace Domain.Users;

public sealed class ApplicationUser : AuditableEntity
{
    private readonly List<RefreshToken> _refreshTokens = [];

    private ApplicationUser() { }

    public ApplicationUser(string email, string passwordHash, IEnumerable<string> roles)
    {
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        RolesCsv = string.Join(',', roles.Select(role => role.Trim()).Where(role => role.Length > 0).Distinct(StringComparer.OrdinalIgnoreCase));
    }

    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string RolesCsv { get; private set; } = string.Empty;
    public IReadOnlyCollection<string> Roles => RolesCsv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public RefreshToken IssueRefreshToken(DateTimeOffset expiresAtUtc)
    {
        var token = new RefreshToken(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), expiresAtUtc);
        _refreshTokens.Add(token);
        return token;
    }
}

public sealed class RefreshToken
{
    private RefreshToken() { }

    public RefreshToken(string token, DateTimeOffset expiresAtUtc)
    {
        Id = Guid.NewGuid();
        Token = token;
        ExpiresAtUtc = expiresAtUtc;
    }

    public Guid Id { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTimeOffset ExpiresAtUtc { get; private set; }
    public bool IsRevoked { get; private set; }
    public bool IsActive(DateTimeOffset now) => !IsRevoked && ExpiresAtUtc > now;
    public void Revoke() => IsRevoked = true;
}
