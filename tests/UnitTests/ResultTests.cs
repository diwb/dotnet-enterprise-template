using Shared.Results;

namespace UnitTests;

public sealed class ResultTests
{
    [Fact]
    public void Failure_requires_a_real_error()
    {
        Assert.Throws<InvalidOperationException>(() => Result.Failure(Error.None));
    }
}
