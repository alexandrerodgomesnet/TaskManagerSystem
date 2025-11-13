namespace TaskManagerSystem.Application.Features.TaskItems.Create;

public record CreateTaskItemRequest(string Title, string? Description, DateTime DueDate, int UserId);