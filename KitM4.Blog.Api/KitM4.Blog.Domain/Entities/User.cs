using KitM4.Blog.Domain.Enums;

namespace KitM4.Blog.Domain.Entities;

public class User : BaseEntity
{
    public required UserRole Role { get; set; }

    public required string Name { get; set; }

    public required string Title { get; set; }

    public string? Bio { get; set; }

    public required string PasswordSalt { get; set; }

    public required string PasswordHash { get; set; }
}