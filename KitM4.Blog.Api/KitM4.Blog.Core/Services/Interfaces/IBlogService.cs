using KitM4.Blog.Domain.Communication.Requests;
using KitM4.Blog.Domain.Communication.Responses;

using System.Security.Claims;

namespace KitM4.Blog.Core.Services.Interfaces;

public interface IBlogService
{
    public Task<AuthorResponse> GetAuthorAsync(CancellationToken ct);

    public Task<List<ArticleResponse>> GetAllArticlesAsync(CancellationToken ct);

    public Task PublishArticleAsync(ClaimsPrincipal claims, BlogRequests.PublishArticle request, CancellationToken ct);
}