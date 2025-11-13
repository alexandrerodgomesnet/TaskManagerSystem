using TaskManagerSystem.Application.Features.TaskItems.Create;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Interfaces.Services;

public interface ITaskItemService
{
    Task<CreateTaskItemResponse> CreateAsync(CreateTaskItemRequest req, CancellationToken cancellationToken);
    Task<IEnumerable<TaskItem>> ListTasksTasksByUser(int userId); 
    Task<bool> CompleteTaskAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id,  CancellationToken cancellationToken);
}