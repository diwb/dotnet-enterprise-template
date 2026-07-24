using Application.Abstractions;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }

    public static async Task SeedAsync(IServiceProvider serviceProvider, Func<string, string> hashPassword)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        if (!await dbContext.Users.AnyAsync())
        {
            dbContext.Users.Add(new ApplicationUser("admin@enterprise.local", hashPassword("Admin123!"), ["Admin"]));
            await dbContext.SaveChangesAsync();
        }
    }
}
