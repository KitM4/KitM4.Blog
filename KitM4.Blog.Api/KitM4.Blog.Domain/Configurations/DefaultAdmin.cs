namespace KitM4.Blog.Domain.Configurations;

public class DefaultAdmin
{
    public string Name { get; set; } = string.Empty;

    public string PasswordSalt { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
}