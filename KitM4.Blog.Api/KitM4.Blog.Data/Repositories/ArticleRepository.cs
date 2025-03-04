using KitM4.Blog.Data.Repositories.Interfaces;
using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace KitM4.Blog.Data.Repositories;

public class ArticleRepository(DatabaseContext database) : IArticleRepository
{
    private readonly DbSet<Article> _articles = database.Articles;

    public Task<List<Article>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Article> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Article entity, CancellationToken ct)
    {
        await _articles.AddAsync(entity, ct);
        await database.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(Article entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Article>> GetAllByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await _articles
            .AsNoTracking()
            .Where(article => article.UserId == userId)
            .Include(article => article.User)
            .Include(article => article.Rates)
            .Include(article => article.Tags)
            .Include(article => article.Comments)
                .ThenInclude(comment => comment.User)
            .AsSplitQuery()
            .ToListAsync(ct);
    }
}