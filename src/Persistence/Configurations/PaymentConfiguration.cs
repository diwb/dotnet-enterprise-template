using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(payment => payment.Id);
        builder.Property(payment => payment.Amount).HasPrecision(18, 2);
        builder.Property(payment => payment.Method).HasMaxLength(80).IsRequired();
        builder.Property(payment => payment.ExternalReference).HasMaxLength(120).IsRequired();
        builder.Property(payment => payment.Status).HasConversion<string>().HasMaxLength(32);
    }
}
