using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Persistence.Context;

public class TaskManagerContext : DbContext
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
}