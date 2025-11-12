using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Repositories.Specifications;

public class TaskItemNotCompletedSpecification: BaseSpecification<TaskItem>
{
    public TaskItemNotCompletedSpecification(Guid id)
    {
        Criteria = t => !t.IsCompleted && t.Id == id;
    }
}