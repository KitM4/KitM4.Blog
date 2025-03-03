using KitM4.Blog.Core.Jwt;
using KitM4.Blog.Core.Mappers;
using KitM4.Blog.Core.Cryptography;
using KitM4.Blog.Core.Services.Interfaces;
using KitM4.Blog.Data.Repositories.Interfaces;
using KitM4.Blog.Domain.Enums;
using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Entities;
using KitM4.Blog.Domain.Exceptions;
using KitM4.Blog.Domain.Configurations;
using KitM4.Blog.Domain.Communication.Requests;
using KitM4.Blog.Domain.Communication.Responses;

using Microsoft.Extensions.Options;
using System.Security.Claims;

using FluentValidation;
using FluentValidation.Results;

namespace KitM4.Blog.Core.Services;

public class AuthService(
    IUserRepository users,
    IOptions<JwtSettings> jwtOptions,
    IValidator<AuthRequests.Register> registerValidator,
    IValidator<AuthRequests.Login> loginValidator,
    IValidator<AuthRequests.ChangeRole> changeRoleValidator) : IAuthService
{
    public async Task<UserResponse> RegisterAsync(AuthRequests.Register request, CancellationToken ct)
    {
        await ValidateRequestAsync(request, registerValidator, ct);

        if (await users.IsExistAsync(request.Name, ct))
        {
            throw new AlreadyExistException(nameof(User), request.Name);
        }

        string salt = HashGenerator.GenerateSalt();
        string hash = HashGenerator.GenerateHash(request.Password, salt);

        User user = new()
        {
            Id = Guid.CreateVersion7(),
            Role = UserRole.User,
            Name = request.Name,
            Title = request.Title,
            Bio = request.Bio,
            PasswordSalt = salt,
            PasswordHash = hash,
            CreatedAt = DateTime.UtcNow,
        };

        await users.AddAsync(user, ct);

        string token = JwtGenerator.GenerateToken(user.Id.ToString(), user.Role.ToString(), jwtOptions.Value);
        return user.ToResponse(token);
    }

    public async Task<UserResponse> LoginAsync(AuthRequests.Login request, CancellationToken ct)
    {
        await ValidateRequestAsync(request, loginValidator, ct);

        User user = await users.GetByNameAsync(request.Name, ct);

        if (user.PasswordHash == HashGenerator.GenerateHash(request.Password, user.PasswordSalt))
        {
            string token = JwtGenerator.GenerateToken(user.Id.ToString(), user.Role.ToString(), jwtOptions.Value);
            return user.ToResponse(token);
        }
        else
        {
            throw new IncorrectCredentialsException();
        }
    }

    public async Task ChangeRoleAsync(AuthRequests.ChangeRole request, CancellationToken ct)
    {
        await ValidateRequestAsync(request, changeRoleValidator, ct);
        await users.ChangeRoleAsync(request.UserId, request.NewRole, ct);
    }

    public async Task DeleteAsync(ClaimsPrincipal claims, CancellationToken ct)
    {
        string idString = JwtGenerator.ExtractClaimFromToken(claims, JwtClaimNames.UserId);

        if (Guid.TryParse(idString, out Guid id))
        {
            await users.DeleteAsync(id, ct);
        }
        else
        {
            throw new ArgumentException(ErrorMessages.InvalidUserId);
        }
    }

    #region Reusable Methods

    private static async Task ValidateRequestAsync<T>(T request, IValidator<T> validator, CancellationToken ct) where T : class
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            throw new InvalidRequestDataException(validationResult.Errors.Select(failure => failure.ErrorMessage));
        }
    }

    #endregion
}