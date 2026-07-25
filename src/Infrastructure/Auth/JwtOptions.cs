namespace Infrastructure.Auth;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = "dotnet-enterprise-template";
    public string Audience { get; init; } = "dotnet-enterprise-template";
    public string SigningKey { get; init; } = "replace-with-a-strong-development-secret";
    public int AccessTokenMinutes { get; init; } = 30;
}
