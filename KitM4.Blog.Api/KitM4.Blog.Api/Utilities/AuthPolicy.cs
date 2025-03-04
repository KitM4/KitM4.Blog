using KitM4.Blog.Domain.Enums;

namespace KitM4.Blog.Api.Utilities;

/// <summary>
/// Defines authorization policies and their associated roles
/// </summary>
public static class AuthPolicy
{
    /// <summary>
    /// Policy that allows access to all authorized users
    /// </summary>
    public const string All = nameof(All);

    /// <summary>
    /// Policy that allows access to moderation-related roles
    /// </summary>
    public const string Moderation = nameof(Moderation);

    /// <summary>
    /// Policy that allows access only to administrators
    /// </summary>
    public const string Admin = nameof(Admin);

    /// <summary>
    /// Dictionary mapping policies to the roles that are allowed to access them
    /// </summary>
    public static Dictionary<string, string[]> Roles => new()
    {
        { All, [UserRole.User.ToString(), UserRole.Moderator.ToString(), UserRole.Admin.ToString()] },
        { Moderation, [UserRole.Admin.ToString(), UserRole.Moderator.ToString()] },
        { Admin, [UserRole.Admin.ToString()] },
    };
}