namespace TaskManagerSystem.Application.Features.TaskItems.Create;

public record CreateTaskItemResponse(Guid Id, string Title, string Description,
    DateTime CreatedAt, DateTime DueDate, int UserId, bool IsCompleted);