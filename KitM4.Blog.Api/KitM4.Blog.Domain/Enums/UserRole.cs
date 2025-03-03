using System.Text.Json.Serialization;

namespace KitM4.Blog.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    User = 0,
    Moderator = 1,
    Admin = 2,
}