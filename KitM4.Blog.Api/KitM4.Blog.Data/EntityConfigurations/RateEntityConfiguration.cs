using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitM4.Blog.Data.EntityConfigurations;

internal class RateEntityConfiguration : BaseEntityConfiguration<Rate>
{
    public override void Configure(EntityTypeBuilder<Rate> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(rate => rate.Article)
            .WithMany(article => article.Rates)
            .HasForeignKey(rate => rate.ArticleId);

        builder
            .HasOne(rate => rate.User)
            .WithMany(user => user.Rates)
            .HasForeignKey(rate => rate.UserId);
    }
}