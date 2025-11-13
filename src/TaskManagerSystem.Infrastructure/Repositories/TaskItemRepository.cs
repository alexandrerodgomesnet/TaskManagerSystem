using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Infrastructure.Persistence.Context;
using TaskManagerSystem.Infrastructure.Repositories.Base;
using TaskManagerSystem.Infrastructure.Repositories.Specifications;

namespace TaskManagerSystem.Infrastructure.Repositories;

public class TaskItemRepository(TaskManagerContext context) 
    : RepositoryBase<TaskItem>(context), ITaskItemRepository
{

    public async Task<IEnumerable<TaskItem>> ListTasksTasksByUser(int userId)
    {
        var spec = new ListTasksByUserIdSpecification(userId);
        var taskItems = await FindAsync(spec);

        if(taskItems == null || !taskItems.Any())
            throw new KeyNotFoundException("List Tasks by user not found.");        

        return taskItems;
    }

    public async Task MarkAsCompleted(Guid id)
    {
        var spec = new TaskNotCompletedSpecification(id);
        var taskItems = await FindAsync(spec);

        var taskItem = taskItems.FirstOrDefault()
            ?? throw new KeyNotFoundException("Task item not found.");
            
        taskItem!.MarkAsCompleted();

        await UpdateAsync(taskItem);
    }
}