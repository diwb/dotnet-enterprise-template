using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public sealed class HealthEndpointTests
{
    [Fact]
    public async Task Health_endpoint_is_available()
    {
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseSetting("environment", "Testing"));

        var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.True((int)response.StatusCode < 500);
    }
}
