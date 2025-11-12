using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.TaskItems.CreateTaskItem;

public record CreateTaskItemRequest(string Title, string? Description, DateTime DueDate, User User);
