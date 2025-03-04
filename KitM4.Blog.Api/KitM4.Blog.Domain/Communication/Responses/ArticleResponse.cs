namespace KitM4.Blog.Domain.Communication.Responses;

public class ArticleResponse
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Category { get; set; }

    public required string MarkdownContent { get; set; }

    public required Guid AuthorId { get; set; }

    public required string AuthorName { get; set; }

    public required string AuthorTitle { get; set; }

    public string? AuthorProfileImageUrl { get; set; }

    public required List<CommentResponse> Comments { get; set; }

    public required List<string> Tags { get; set; }

    public required int RatesCount { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}