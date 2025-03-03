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
            .HasKey(user => user.Id);

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
            .Property(user => user.Role)
            .HasConversion<string>()
            .IsRequired();
    }
}