using Domain.Users;

namespace Application.Abstractions;

public interface IAuthTokenService
{
    string CreateAccessToken(ApplicationUser user);
}
