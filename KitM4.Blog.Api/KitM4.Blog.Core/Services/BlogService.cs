using KitM4.Blog.Core.Jwt;
using KitM4.Blog.Core.Services.Interfaces;
using KitM4.Blog.Data.Repositories.Interfaces;
using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Entities;
using KitM4.Blog.Domain.Communication.Requests;
using KitM4.Blog.Domain.Communication.Responses;

using System.Security.Claims;

namespace KitM4.Blog.Core.Services;

public class BlogService(IUserRepository users, IArticleRepository articles) : IBlogService
{
    public async Task<AuthorResponse> GetAuthorAsync(CancellationToken ct)
    {
        User author = await users.GetAuthorAsync(ct);

        return new()
        {
            Id = author.Id,
            Name = author.Name,
            Title = author.Title,
            ProfileImageUrl = author.ProfileImageUrl,
            Bio = author.Bio,
            ArticleDetails = author.Articles
                .GroupBy(article => article.Category)
                .Select(group => new ArticleDetailsResponse()
                {
                    Category = group.Key,
                    ArticleTitles = group.Select(article => article.Title).ToList(),
                    RatesCount = group.Sum(article => article.Rates.Count),
                })
                .ToList(),
        };
    }

    public async Task<List<ArticleResponse>> GetAllArticlesAsync(CancellationToken ct)
    {
        Guid authorId = await users.GetAuthorId(ct);
        List<Article> authorArticles = await articles.GetAllByUserIdAsync(authorId, ct);

        return authorArticles
            .Select(article => new ArticleResponse()
            {
                Id = article.Id,
                Title = article.Title,
                Category = article.Category,
                MarkdownContent = article.MarkdownContent,
                AuthorId = article.User.Id,
                AuthorName = article.User.Name,
                AuthorTitle = article.User.Title,
                AuthorProfileImageUrl = article.User.ProfileImageUrl,
                Comments = article.Comments
                    .Select(comment => new CommentResponse()
                    {
                        Id = comment.Id,
                        Text = comment.Text,
                        AuthorId = comment.User.Id,
                        AuthorName = comment.User.Name,
                        AuthorProfileImageUrl = comment.User.ProfileImageUrl,
                        CreatedAt = comment.CreatedAt,
                        UpdatedAt = comment.UpdatedAt,
                    })
                    .ToList(),
                Tags = article.Tags
                    .Select(tag => tag.Name)
                    .ToList(),
                RatesCount = article.Rates.Count,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
            })
            .ToList();
    }

    public async Task PublishArticleAsync(ClaimsPrincipal claims, BlogRequests.PublishArticle request, CancellationToken ct)
    {
        // TODO: validate

        if (!Guid.TryParse(JwtGenerator.ExtractClaimFromToken(claims, JwtClaimNames.UserId), out Guid userId))
        {
            throw new ArgumentException(ErrorMessages.InvalidUserId);
        }

        List<Tag> tags = [];
        Guid articleId = Guid.CreateVersion7();
        DateTime now = DateTime.UtcNow;

        foreach (string tag in request.Tags)
        {
            tags.Add(new()
            {
                Id = Guid.CreateVersion7(),
                Name = tag.Trim().ToLowerInvariant(),
                ArticleId = articleId,
                CreatedAt = now,
            });
        }

        Article article = new()
        {
            Id = articleId,
            Title = request.Title,
            Category = request.Category,
            MarkdownContent = request.MarkdownContent,
            UserId = userId,
            Comments = [],
            Tags = tags,
            Rates = [],
            CreatedAt = now,
        };

        await articles.AddAsync(article, ct);
    }
}