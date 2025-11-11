using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(t => t.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(200);

        // Relacionamento com TaskItem (1:N)
        builder.HasMany<TaskItem>()
            .WithOne(t => t.User)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);


        // builder.Property(t => t.DateOfCreated)
        //     .HasDefaultValueSql("CURRENT_TIMESTAMP")
        //     .IsRequired()
        //     .HasColumnType("datetime2");
        // builder.Property(t => t.DateOfUpdate)
        //     .HasColumnType("datetime2")
        //     .IsRequired(false);
        // builder.Property(t => t.IsCompleted)
        //     .IsRequired();
        // builder.HasIndex(t => t.Title)
        //     .HasDatabaseName("IX_Tarefas_Title");
    }
}
