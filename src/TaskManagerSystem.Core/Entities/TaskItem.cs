namespace TaskManagerSystem.Core.Entities;

public class TaskItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public int UserId { get; set; }
    public bool IsCompleted { get; set; } = false;

    public void MarkAsCompleted() => IsCompleted = true;
}