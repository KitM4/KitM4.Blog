using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitM4.Blog.Data.EntityConfigurations;

internal class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder
            .Property(user => user.Role)
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(user => user.Name)
            .HasMaxLength(EntityDataLength.MaxNameLength)
            .IsRequired();

        builder
            .HasIndex(user => user.Name)
            .IsUnique();

        builder
            .Property(user => user.Title)
            .HasMaxLength(EntityDataLength.MaxTitleLength)
            .IsRequired();

        builder
            .Property(user => user.ProfileImageUrl)
            .HasMaxLength(EntityDataLength.MaxUrlLength);

        builder
            .Property(user => user.Bio)
            .HasMaxLength(EntityDataLength.MaxBioLength);

        builder
            .Property(user => user.PasswordSalt)
            .HasMaxLength(EntityDataLength.MaxHashLength)
            .IsRequired();

        builder
            .Property(user => user.PasswordHash)
            .HasMaxLength(EntityDataLength.MaxHashLength)
            .IsRequired();

        builder
            .HasMany(user => user.Articles)
            .WithOne(article => article.User)
            .HasForeignKey(article => article.UserId);

        builder
            .HasMany(user => user.Comments)
            .WithOne(comment => comment.User)
            .HasForeignKey(comment => comment.UserId);

        builder
            .HasMany(user => user.Rates)
            .WithOne(rate => rate.User)
            .HasForeignKey(rate => rate.UserId);
    }
}