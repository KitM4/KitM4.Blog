using KitM4.Blog.Domain.Entities;
using KitM4.Blog.Domain.Communication.Responses;

namespace KitM4.Blog.Core.Mappers;

public static class UserMapper
{
    public static UserResponse ToResponse(this User user, string? token = null)
    {
        return new()
        {
            Id = user.Id,
            Role = user.Role,
            Name = user.Name,
            Title = user.Title,
            CreatedAt = user.CreatedAt,
            Bio = user.Bio,
            Token = token,
        };
    }
}