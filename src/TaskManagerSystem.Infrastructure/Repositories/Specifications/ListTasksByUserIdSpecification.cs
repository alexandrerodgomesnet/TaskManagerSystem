using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Infrastructure.Repositories.Specifications;

public class ListTasksByUserIdSpecification : BaseSpecification<TaskItem>
{
    public ListTasksByUserIdSpecification(int userId)
    {
        Criteria = t => t.UserId == userId;
        
        ApplyOrderByDescending(t => t.Id);
    }
}