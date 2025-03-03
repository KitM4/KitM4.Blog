using KitM4.Blog.Domain.Enums;
using KitM4.Blog.Domain.Entities;
using KitM4.Blog.Domain.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KitM4.Blog.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceScope scope)
    {
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.EnsureCreated();

        if (!await context.Users.AnyAsync(user => user.Role == UserRole.Admin))
        {
            IOptions<DefaultAdmin> defaultAdminOptions = scope.ServiceProvider.GetRequiredService<IOptions<DefaultAdmin>>();

            await SeedDefaultAdmin(context, defaultAdminOptions.Value);
        }
    }

    private static async Task SeedDefaultAdmin(DatabaseContext context, DefaultAdmin defaultAdmin)
    {
        User admin = new()
        {
            Id = Guid.CreateVersion7(),
            Role = UserRole.Admin,
            Name = defaultAdmin.Name,
            Title = "Default Admin",
            PasswordSalt = defaultAdmin.PasswordSalt,
            PasswordHash = defaultAdmin.PasswordHash,
            CreatedAt = DateTime.UtcNow,
        };

        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}