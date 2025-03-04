using KitM4.Blog.Domain.Entities;

namespace KitM4.Blog.Data.Repositories.Interfaces;

public interface IArticleRepository : IRepository<Article>
{
    public Task<List<Article>> GetAllByUserIdAsync(Guid userId, CancellationToken ct);
}