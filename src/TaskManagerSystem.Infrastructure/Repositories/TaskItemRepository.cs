using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Infrastructure.Persistence.Context;
using TaskManagerSystem.Infrastructure.Repositories.Base;
using TaskManagerSystem.Infrastructure.Repositories.Specifications;

namespace TaskManagerSystem.Infrastructure.Repositories;

public class TaskItemRepository(TaskManagerContext context) 
    : RepositoryBase<TaskItem>(context), ITaskItemRepository
{

    public async Task<IEnumerable<TaskItem>> GetIsCompletedAsync()
    {
        var spec = new TaskItemCompletedSpecification();
        var tarefas = await FindAsync(spec);

        return tarefas;
    }

    public async Task MarkAsCompleted(Guid id)
    {
        var spec = new TaskItemNotCompletedSpecification(id);
        var tarefas = await FindAsync(spec);

        var tarefa = tarefas.FirstOrDefault()
            ?? throw new KeyNotFoundException("Task not found.");
            
        tarefa!.MarkAsCompleted();

        await UpdateAsync(tarefa);
    }
}