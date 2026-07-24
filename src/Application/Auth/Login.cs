using Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace Application.Auth;

public sealed record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;

public sealed class LoginHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher, IAuthTokenService tokenService)
    : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email.ToLower(), cancellationToken);
        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result.Failure<AuthResponse>(new Error("auth.invalid_credentials", "Invalid email or password."));

        var refreshToken = user.IssueRefreshToken(DateTimeOffset.UtcNow.AddDays(7));
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponse(tokenService.CreateAccessToken(user), refreshToken.Token, DateTimeOffset.UtcNow.AddMinutes(30)));
    }
}
