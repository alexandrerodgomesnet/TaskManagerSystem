using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Persistence.Context;

public class TaskManagerSystemDb : DbContext
{
    public TaskManagerSystemDb(DbContextOptions<TaskManagerSystemDb> options)
        : base(options) { }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerSystemDb).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
}