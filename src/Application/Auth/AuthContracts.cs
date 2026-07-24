namespace Application.Auth;

public sealed record LoginRequest(string Email, string Password);
public sealed record AuthResponse(string AccessToken, string RefreshToken, DateTimeOffset ExpiresAtUtc);
