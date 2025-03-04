namespace KitM4.Blog.Domain.Configurations;

public class DefaultAdmin
{
    public required string Name { get; set; }

    public required string Title { get; set; }

    public string? ProfileImageUrl { get; set; }

    public string? Bio { get; set; }

    public required string PasswordSalt { get; set; }

    public required string PasswordHash { get; set; }
}