using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Core.Requests.TaskItems;
using TaskManagerSystem.Core.Responses.TaskItems;

namespace TaskManagerSystem.Application.Interfaces.Services;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<TaskItemResponse> AddAsync(TaskItemRequest req, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, TaskItemRequest req, CancellationToken cancellationToken);
    Task<bool> MarkAsCompleted(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id,  CancellationToken cancellationToken);
}