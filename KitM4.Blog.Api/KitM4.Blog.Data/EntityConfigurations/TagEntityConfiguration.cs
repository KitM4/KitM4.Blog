using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitM4.Blog.Data.EntityConfigurations;

internal class TagEntityConfiguration : BaseEntityConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);

        builder
            .Property(tag => tag.Name)
            .HasMaxLength(EntityDataLength.MaxNameLength)
            .IsRequired();

        builder
            .HasOne(tag => tag.Article)
            .WithMany(article => article.Tags)
            .HasForeignKey(tag => tag.ArticleId);
    }
}