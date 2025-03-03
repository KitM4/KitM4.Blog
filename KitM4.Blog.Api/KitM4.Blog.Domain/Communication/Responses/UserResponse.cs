using KitM4.Blog.Domain.Enums;

using System.Text.Json.Serialization;

namespace KitM4.Blog.Domain.Communication.Responses;

public class UserResponse
{
    public required Guid Id { get; set; }

    public required UserRole Role { get; set; }

    public required string Name { get; set; }

    public required string Title { get; set; }

    public required DateTime CreatedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Bio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; set; }
}