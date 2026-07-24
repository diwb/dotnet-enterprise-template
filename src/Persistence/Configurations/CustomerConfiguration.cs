using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);
        builder.Property(customer => customer.LegalName).HasMaxLength(160).IsRequired();
        builder.Property(customer => customer.Document).HasMaxLength(32).IsRequired();
        builder.HasIndex(customer => customer.Document).IsUnique();
        builder.Property(customer => customer.Email).HasMaxLength(254).IsRequired();
        builder.OwnsOne(customer => customer.BillingAddress, address =>
        {
            address.Property(x => x.Street).HasMaxLength(120).IsRequired();
            address.Property(x => x.Number).HasMaxLength(20).IsRequired();
            address.Property(x => x.District).HasMaxLength(80).IsRequired();
            address.Property(x => x.City).HasMaxLength(80).IsRequired();
            address.Property(x => x.State).HasMaxLength(80).IsRequired();
            address.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
            address.Property(x => x.Country).HasMaxLength(80).IsRequired();
        });
    }
}
