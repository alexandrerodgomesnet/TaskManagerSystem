namespace TaskManagerSystem.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    ITaskItemRepository TaskItems { get; }
    Task<bool> CommitAsync(CancellationToken cancellationToken);
    void Dispose();
}