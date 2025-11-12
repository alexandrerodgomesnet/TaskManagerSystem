using TaskManagerSystem.Application.Interfaces.Repositories.Specification;

namespace TaskManagerSystem.Application.Interfaces.Repositories.Base;

public interface IRepositoryBase<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(ISpecification<T> spec);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}