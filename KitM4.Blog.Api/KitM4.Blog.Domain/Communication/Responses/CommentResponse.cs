namespace KitM4.Blog.Domain.Communication.Responses;

public class CommentResponse
{
    public required Guid Id { get; set; }

    public required string Text { get; set; }

    public required Guid AuthorId { get; set; }

    public required string AuthorName { get; set; }

    public string? AuthorProfileImageUrl { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}