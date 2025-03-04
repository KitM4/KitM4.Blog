namespace KitM4.Blog.Domain.Entities;

public class Tag : BaseEntity
{
    public required string Name { get; set; }

    public required Guid ArticleId { get; set; }

    public Article Article { get; set; } = null!;
}