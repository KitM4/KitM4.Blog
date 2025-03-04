namespace KitM4.Blog.Domain.Communication.Responses;

public class AllArticlesResponse
{
    public required Guid AuthorId { get; set; }

    public required string AuthorName { get; set; }

    public required string AuthorTitle { get; set; }

    public string? AuthorProfileImageUrl { get; set; }

    public required List<ArticleResponse> Articles { get; set; }
}