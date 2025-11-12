namespace TaskManagerSystem.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    ITaskItemRepository TaskItems { get; }
    IUserRepository Users { get; } 
    Task<bool> CommitAsync(CancellationToken cancellationToken);
    void Dispose();
}