using KitM4.Blog.Domain.Communication.Requests;
using KitM4.Blog.Domain.Communication.Responses;

using System.Security.Claims;

namespace KitM4.Blog.Core.Services.Interfaces;

public interface IAuthService
{
    public Task<UserResponse> RegisterAsync(AuthRequests.Register request, CancellationToken ct);

    public Task<UserResponse> LoginAsync(AuthRequests.Login request, CancellationToken ct);

    public Task DeleteAsync(ClaimsPrincipal claims, CancellationToken ct);

    public Task ChangeRoleAsync(AuthRequests.ChangeRole request, CancellationToken ct);
}