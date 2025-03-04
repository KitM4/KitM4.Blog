using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitM4.Blog.Data.EntityConfigurations;

internal class ArticleEntityConfiguration : BaseEntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder
            .Property(article => article.Title)
            .HasMaxLength(EntityDataLength.MaxTitleLength)
            .IsRequired();

        builder
            .Property(article => article.Category)
            .HasMaxLength(EntityDataLength.MaxNameLength)
            .IsRequired();

        builder
            .Property(article => article.MarkdownContent)
            .IsRequired();

        builder
            .HasOne(article => article.User)
            .WithMany(user => user.Articles)
            .HasForeignKey(article => article.UserId);

        builder
            .HasMany(article => article.Tags)
            .WithOne(tag => tag.Article)
            .HasForeignKey(tag => tag.ArticleId);

        builder
            .HasMany(article => article.Rates)
            .WithOne(rate => rate.Article)
            .HasForeignKey(rate => rate.ArticleId);
    }
}