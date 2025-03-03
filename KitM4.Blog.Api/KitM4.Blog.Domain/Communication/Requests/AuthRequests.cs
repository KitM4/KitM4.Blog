using KitM4.Blog.Domain.Enums;

namespace KitM4.Blog.Domain.Communication.Requests;

public class AuthRequests
{
    public record Register(string Name, string Title, string? Bio, string Password);

    public record Login(string Name, string Password);

    public record ChangeRole(Guid UserId, UserRole NewRole);
}