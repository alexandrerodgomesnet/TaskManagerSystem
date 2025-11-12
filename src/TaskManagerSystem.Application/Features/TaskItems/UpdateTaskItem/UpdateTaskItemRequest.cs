using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.TaskItems.UpdateTaskItem;

public class UpdateTaskItemRequest
{
    private UpdateTaskItemRequest(string title, string? description, DateTime dueDate, User user)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        User = user;
    }

    public static UpdateTaskItemRequest Create(string title, string? description,
        DateTime dueDate, User user) =>
            new(title, description, dueDate, user);
            
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public DateTime DueDate { get; private set; }
    public User User { get; private set; } = null!;
}