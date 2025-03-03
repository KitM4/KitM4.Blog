using KitM4.Blog.Domain.Entities;

namespace KitM4.Blog.Data.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAllAsync(CancellationToken ct);

    public Task<T> GetByIdAsync(Guid id, CancellationToken ct);

    public Task AddAsync(T entity, CancellationToken ct);

    public Task UpdateAsync(T entity, CancellationToken ct);

    public Task DeleteAsync(Guid id, CancellationToken ct);
}