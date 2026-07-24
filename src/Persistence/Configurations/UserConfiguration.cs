using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Email).HasMaxLength(254).IsRequired();
        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(user => user.PasswordHash).IsRequired();
        builder.Property(user => user.RolesCsv).HasColumnName("Roles").HasMaxLength(200).IsRequired();
        builder.HasMany(user => user.RefreshTokens).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
