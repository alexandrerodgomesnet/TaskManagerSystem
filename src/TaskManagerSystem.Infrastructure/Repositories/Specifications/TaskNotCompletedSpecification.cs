using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Repositories.Specifications;

public class TaskNotCompletedSpecification : BaseSpecification<TaskItem>
{
    public TaskNotCompletedSpecification(Guid id)
    {
        Criteria = t => !t.IsCompleted && t.Id == id;
    }
}