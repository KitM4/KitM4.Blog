using KitM4.Blog.Domain.Common;
using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitM4.Blog.Data.EntityConfigurations;

internal class CommentEntityConfiguration : BaseEntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder
            .Property(comment => comment.Text)
            .HasMaxLength(EntityDataLength.MaxCommentLength)
            .IsRequired();

        builder
            .HasOne(comment => comment.Article)
            .WithMany(article => article.Comments)
            .HasForeignKey(comment => comment.ArticleId);

        builder
            .HasOne(comment => comment.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.UserId);
    }
}