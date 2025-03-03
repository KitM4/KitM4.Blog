using KitM4.Blog.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitM4.Blog.Data.EntityConfigurations;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .HasKey(entity => entity.Id);

        builder
            .Property(entity => entity.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder
            .Property(entity => entity.UpdatedAt)
            .HasColumnType("datetime2");
    }
}