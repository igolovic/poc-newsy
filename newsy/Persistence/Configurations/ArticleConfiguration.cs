using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(nameof(Article));

            builder.HasKey(article => article.Id);

            builder.Property(article => article.Id).ValueGeneratedOnAdd();

            builder.Property(article => article.Created).IsRequired();

            builder.Property(article => article.Content).HasMaxLength(32500);
        }
    }
}
