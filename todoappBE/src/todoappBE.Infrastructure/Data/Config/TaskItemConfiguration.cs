using todoappBE.Core.Models;
using todoappBE.Infrastructure.Data.Config;
using todoAppBE.Core.UserAggregate;

namespace TodoAppBE.Infrastructure.Data.Config;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
  public void Configure(EntityTypeBuilder<TaskItem> builder)
  {
    builder.Property(t => t.Title)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();

    builder.Property(t => t.Description)
        .HasMaxLength(DataSchemaConstants.DEFAULT_DESCRIPTION_LENGTH);

    builder.Property(t => t.CreatedAt)
        .IsRequired();

    builder.Property(t => t.Status)
        .HasConversion<string>()
        .IsRequired();

    builder.Property(t => t.DueDate);

    // Relation with User
    builder.Property<int>("UserId")
        .IsRequired();

    // Relation with Tags
    builder.HasMany<Tag>()
        .WithMany()
        .UsingEntity<TaskTag>(
            j => j.HasOne<Tag>().WithMany().HasForeignKey(tt => tt.TagId),
            j => j.HasOne<TaskItem>().WithMany().HasForeignKey(tt => tt.TaskId),
            j =>
            {
              j.ToTable("TaskTag");
              j.HasKey(tt => new { tt.TaskId, tt.TagId });
            }
        );
  }
}
