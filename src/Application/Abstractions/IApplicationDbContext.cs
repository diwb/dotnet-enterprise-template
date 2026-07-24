using Domain.Customers;
using Domain.Orders;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Order> Orders { get; }
    DbSet<ApplicationUser> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
