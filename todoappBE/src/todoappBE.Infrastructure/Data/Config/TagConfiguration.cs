using todoappBE.Infrastructure.Data.Config;
using todoAppBE.Core.UserAggregate;

namespace TodoAppBE.Infrastructure.Data.Config;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
  public void Configure(EntityTypeBuilder<Tag> builder)
  {
    builder.Property(t => t.Name)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();

    // Relación con User
    builder.Property<int>("UserId")
        .IsRequired();
  }
}
