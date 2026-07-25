using System.Security.Claims;
using Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure;

public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string UserId =>
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? "system";
}
