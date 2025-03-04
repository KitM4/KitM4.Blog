namespace KitM4.Blog.Domain.Entities;

public class Comment : BaseEntity
{
    public required string Text { get; set; }

    public required Guid ArticleId { get; set; }

    public Article Article { get; set; } = null!;

    public required Guid UserId { get; set; }

    public User User { get; set; } = null!;
}