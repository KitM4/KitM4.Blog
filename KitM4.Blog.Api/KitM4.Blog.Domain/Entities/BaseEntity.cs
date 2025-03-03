namespace KitM4.Blog.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}