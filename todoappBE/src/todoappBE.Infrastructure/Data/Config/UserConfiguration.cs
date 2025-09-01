using todoappBE.Infrastructure.Data.Config;
using todoAppBE.Core.UserAggregate;

namespace TodoAppBE.Infrastructure.Data.Config;

// Add-Migration InitialCreation -Project todoappBE.Infrastructure -StartupProject todoappBE.Web -OutputDir Data\Migrations
// Update-Database -Project todoappBE.Infrastructure -StartupProject todoappBE.Web

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(u => u.Name)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();

    builder.Property(u => u.Email)
        .HasMaxLength(DataSchemaConstants.DEFAULT_EMAIL_LENGTH)
        .IsRequired();

    builder.HasIndex(u => u.Email)
        .IsUnique();

    builder.Property(u => u.PasswordHash)
        .IsRequired();

    builder.Property(u => u.CreatedAt)
        .IsRequired();

    // TaskItem
    builder.HasMany(u => u.Tasks)
        .WithOne() // No Navigation (User is root)
        .HasForeignKey("UserId")
        .OnDelete(DeleteBehavior.Cascade);

    // Tag
    builder.HasMany(u => u.Tags)
        .WithOne() // No Navigation (User is root)
        .HasForeignKey("UserId")
        .OnDelete(DeleteBehavior.Cascade);
  }
}
