using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KitM4.Blog.Data;

public class DatabaseContext(IConfiguration configuration, DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Article> Articles => Set<Article>();

    public DbSet<Comment> Comments => Set<Comment>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<Rate> Rate => Set<Rate>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: implement configurable database

        optionsBuilder.UseSqlite(configuration.GetConnectionString("Sqlite"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
}