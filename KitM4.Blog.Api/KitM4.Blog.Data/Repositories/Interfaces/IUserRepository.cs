using KitM4.Blog.Domain.Enums;
using KitM4.Blog.Domain.Entities;

namespace KitM4.Blog.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetAuthorAsync(CancellationToken ct);

    public Task<User> GetByNameAsync(string name, CancellationToken ct);

    public Task<Guid> GetAuthorId(CancellationToken ct);

    public Task<bool> IsExistAsync(string name, CancellationToken ct);

    public Task ChangeRoleAsync(Guid id, UserRole newRole, CancellationToken ct);
}