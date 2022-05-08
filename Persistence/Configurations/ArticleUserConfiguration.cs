using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class ArticleUserConfiguration : IEntityTypeConfiguration<ArticleUser>
    {
        public void Configure(EntityTypeBuilder<ArticleUser> builder)
        {
            builder.ToTable(nameof(ArticleUser));

            builder.HasKey(articleUser => new { articleUser.ArticleId, articleUser.User_Id });

            builder.HasOne(articleUser => articleUser.Article)
                .WithMany(article => article.ArticleUser)
                .HasForeignKey(articleUser => articleUser.ArticleId);

            builder.HasOne(articleUser => articleUser.User)
                .WithMany(user => user.ArticleUser)
                .HasForeignKey(articleUser => articleUser.User_Id);
        }
    }
}
