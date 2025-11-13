using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Infrastructure.Persistence.Context;

namespace TaskManagerSystem.Infrastructure.Repositories;

public class UnitOfWork(TaskManagerContext context, ITaskItemRepository taskItemRepository) : IUnitOfWork
{
    private readonly TaskManagerContext _context = context;
    public ITaskItemRepository TaskItems { get; } = taskItemRepository;

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken) == 1;
    public void Dispose() => _context.Dispose();
}