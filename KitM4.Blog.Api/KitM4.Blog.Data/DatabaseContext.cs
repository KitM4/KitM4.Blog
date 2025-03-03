using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KitM4.Blog.Data;

public class DatabaseContext(IConfiguration configuration, DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
}