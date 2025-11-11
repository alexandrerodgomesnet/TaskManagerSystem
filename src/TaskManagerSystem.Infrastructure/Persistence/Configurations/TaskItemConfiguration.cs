using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.CreatedAt)
            .IsRequired()
           .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(t => t.DueDate)
            .IsRequired(false);

        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);

        // Relacionamento com User (N:1)
        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey("UserId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}