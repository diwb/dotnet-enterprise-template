using Infrastructure.Auth;

namespace UnitTests;

public sealed class PasswordHasherTests
{
    [Fact]
    public void Verify_returns_true_for_original_password()
    {
        var hasher = new PasswordHasher();
        var hash = hasher.Hash("Admin123!");

        Assert.True(hasher.Verify("Admin123!", hash));
    }

    [Fact]
    public void Verify_returns_false_for_wrong_password()
    {
        var hasher = new PasswordHasher();
        var hash = hasher.Hash("Admin123!");

        Assert.False(hasher.Verify("wrong-password", hash));
    }
}
