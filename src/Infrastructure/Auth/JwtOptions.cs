namespace Infrastructure.Auth;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    private const int MinimumSigningKeyLength = 32;

    public string Issuer { get; init; } = "dotnet-enterprise-template";
    public string Audience { get; init; } = "dotnet-enterprise-template";
    public string SigningKey { get; init; } = "replace-with-a-strong-development-secret";
    public int AccessTokenMinutes { get; init; } = 30;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Issuer))
            throw new InvalidOperationException("Jwt:Issuer must be configured.");

        if (string.IsNullOrWhiteSpace(Audience))
            throw new InvalidOperationException("Jwt:Audience must be configured.");

        if (string.IsNullOrWhiteSpace(SigningKey) || SigningKey.Length < MinimumSigningKeyLength)
            throw new InvalidOperationException($"Jwt:SigningKey must contain at least {MinimumSigningKeyLength} characters.");

        if (AccessTokenMinutes <= 0)
            throw new InvalidOperationException("Jwt:AccessTokenMinutes must be greater than zero.");
    }
}
