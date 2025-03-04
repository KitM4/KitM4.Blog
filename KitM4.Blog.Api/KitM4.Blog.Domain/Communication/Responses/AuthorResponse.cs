namespace KitM4.Blog.Domain.Communication.Responses;

public class AuthorResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Title { get; set; }

    public string? ProfileImageUrl { get; set; }

    public string? Bio { get; set; }

    public required List<ArticleDetailsResponse> ArticleDetails { get; set; }
}