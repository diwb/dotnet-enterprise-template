using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Sku).HasMaxLength(64).IsRequired();
        builder.Property(item => item.Description).HasMaxLength(200).IsRequired();
        builder.Property(item => item.UnitPrice).HasPrecision(18, 2);
    }
}
