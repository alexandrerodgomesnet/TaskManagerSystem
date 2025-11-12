using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.TaskItems.UpdateTaskItem
{
    public class UpdateTaskItemResponse
    {
        private UpdateTaskItemResponse(Guid id, string title, string? description,
            DateTime createdAt, DateTime? dueDate, User user, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            DueDate = dueDate;
            User = user;
            IsCompleted = isCompleted;
        }

        public static UpdateTaskItemResponse Create(Guid id, string title, string? description,
            DateTime createdAt, DateTime? dueDate, User user, bool isCompleted) =>
                new (id, title, description, createdAt, dueDate, user, isCompleted);
        
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }= string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; private set; }
        public User User { get; private set; } = null!;
        public bool IsCompleted { get; private set; } = false;
    }
}