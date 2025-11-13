using TaskManagerSystem.Application.Interfaces.Repositories.Base;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Interfaces.Repositories;

public interface ITaskItemRepository : IRepositoryBase<TaskItem>
{
    Task<IEnumerable<TaskItem>> ListTasksTasksByUser(int userId);
    Task MarkAsCompleted(Guid id);
}