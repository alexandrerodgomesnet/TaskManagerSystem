
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Application.Interfaces.Repositories.Base;
using TaskManagerSystem.Application.Interfaces.Repositories.Specification;
using TaskManagerSystem.Infrastructure.Persistence.Context;
using TaskManagerSystem.Infrastructure.Repositories.Specifications;

namespace TaskManagerSystem.Infrastructure.Repositories.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly TaskManagerContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(TaskManagerContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) =>
        await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<T>> FindAsync(ISpecification<T> spec)
    {
        var query = SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken) =>
        await _dbSet.AddAsync(entity, cancellationToken);

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);
}