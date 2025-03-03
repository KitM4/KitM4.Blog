using KitM4.Blog.Domain.Enums;

namespace KitM4.Blog.Api.Utilities;

public static class AuthPolicy
{
    public const string All = nameof(All);

    public const string Moderation = nameof(Moderation);

    public const string Admin = nameof(Admin);

    public static Dictionary<string, string[]> Roles => new()
    {
        { All, [UserRole.User.ToString(), UserRole.Moderator.ToString(), UserRole.Admin.ToString()] },
        { Moderation, [UserRole.Admin.ToString(), UserRole.Moderator.ToString()] },
        { Admin, [UserRole.Admin.ToString()] },
    };
}