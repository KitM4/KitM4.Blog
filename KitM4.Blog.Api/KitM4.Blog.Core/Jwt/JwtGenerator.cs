using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Configurations;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

namespace KitM4.Blog.Core.Jwt;

public static class JwtGenerator
{
    public static string GenerateToken(string userId, string userRole, JwtSettings settings)
    {
        Guid jwtId = Guid.NewGuid();
        byte[] key = System.Text.Encoding.UTF8.GetBytes(settings.Key);

        List<Claim> claims =
        [
            new(JwtClaimNames.JwtId, jwtId.ToString()),
            new(JwtClaimNames.UserId, userId),
            new(JwtClaimNames.UserRole, userRole),
        ];

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new(claims),
            Issuer = settings.Issuer,
            Audience = settings.Audience,
            Expires = DateTime.UtcNow.AddDays(settings.ExpireDays),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public static string ExtractClaimFromToken(ClaimsPrincipal claims, string claimName)
    {
        return claims.FindFirst(claimName)?.Value ??
            throw new ArgumentException(ErrorMessages.InvalidAuthorizationToken);
    }
}