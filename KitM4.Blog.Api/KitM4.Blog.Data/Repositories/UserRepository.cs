using KitM4.Blog.Data.Repositories.Interfaces;
using KitM4.Blog.Domain.Enums;
using KitM4.Blog.Domain.Entities;
using KitM4.Blog.Domain.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace KitM4.Blog.Data.Repositories;

public class UserRepository(DatabaseContext database) : IUserRepository
{
    private readonly DbSet<User> _users = database.Users;

    public Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(User entity, CancellationToken ct)
    {
        await _users.AddAsync(entity, ct);
        await database.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(User entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        User user = await _users
            .FirstOrDefaultAsync(user => user.Id == id, ct) ??
                throw new NotFoundException(nameof(User), id.ToString());

        _users.Remove(user);
        await database.SaveChangesAsync(ct);
    }

    public async Task<User> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Name == name, ct) ??
                throw new NotFoundException(nameof(User), name);
    }

    public async Task<bool> IsExistAsync(string name, CancellationToken ct)
    {
        return await _users
            .AsNoTracking()
            .AnyAsync(user => user.Name == name, ct);
    }

    public async Task ChangeRoleAsync(Guid id, UserRole newRole, CancellationToken ct)
    {
        User user = await _users
            .FirstOrDefaultAsync(user => user.Id == id, ct) ??
                throw new NotFoundException(nameof(User), id.ToString());

        user.Role = newRole;
        await database.SaveChangesAsync(ct);
    }
}