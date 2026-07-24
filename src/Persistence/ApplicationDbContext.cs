using Application.Abstractions;
using Domain.Common;
using Domain.Customers;
using Domain.Orders;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser) : DbContext(options), IApplicationDbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.MarkCreated(currentUser.UserId, now);
            else if (entry.State == EntityState.Modified)
                entry.Entity.MarkUpdated(currentUser.UserId, now);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
