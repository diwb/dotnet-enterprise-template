using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(order => order.Id);
        builder.HasQueryFilter(order => !order.IsDeleted);
        builder.Property(order => order.Status).HasConversion<string>().HasMaxLength(32);
        builder.Ignore(order => order.TotalAmount);
        builder.HasMany(order => order.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(order => order.Payments).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(order => order.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(order => order.Payments).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
