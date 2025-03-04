using KitM4.Blog.Domain.Communication.Requests;

using System.Security.Claims;

namespace KitM4.Blog.Core.Services.Interfaces;

public interface IAuthService
{
    public Task<string> RegisterAsync(AuthRequests.Register request, CancellationToken ct);

    public Task<string> LoginAsync(AuthRequests.Login request, CancellationToken ct);

    public Task DeleteAsync(ClaimsPrincipal claims, CancellationToken ct);

    public Task ChangeRoleAsync(AuthRequests.ChangeRole request, CancellationToken ct);
}