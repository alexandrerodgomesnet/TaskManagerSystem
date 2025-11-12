using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Repositories.Specifications;

public class TaskItemCompletedSpecification : BaseSpecification<TaskItem>
{
    public TaskItemCompletedSpecification()
    {
        Criteria = t => t.IsCompleted;
        ApplyOrderByDescending(t => t.Id);
    }
}