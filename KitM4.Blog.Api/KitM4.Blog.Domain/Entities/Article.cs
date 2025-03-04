namespace KitM4.Blog.Domain.Entities;

public class Article : BaseEntity
{
    public required string Title { get; set; }

    public required string Category { get; set; }

    public required string MarkdownContent { get; set; }

    public required Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public required ICollection<Comment> Comments { get; set; }

    public required ICollection<Tag> Tags { get; set; }

    public required ICollection<Rate> Rates { get; set; }
}