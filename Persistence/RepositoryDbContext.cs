using System.Reflection;
using System.Reflection.Metadata;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Article>? Articles { get; set; }
        
        public DbSet<ArticleUser>? ArticleUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseSeeding((context, _) =>
            {
                var testBlog = context.Set<User>().FirstOrDefault(b => b.Id == new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cda"));
                if (testBlog == null)
                {
                    context.Set<User>().AddRange(
                        new User
                        {
                            Id = new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cda"),
                            FirstName = "Pooh",
                            LastName = "Shiesty",
                            UserName = "ps"
                        },
                        new User
                        {
                            Id = new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9"),
                            FirstName = "T",
                            LastName = "Rex",
                            UserName = "tr"
                        }
                    );

                    context.Set<Article>().AddRange(
                        new Article
                        {
                            Id = new Guid("5ad823ec-e2fa-4a4a-aec8-914c7298661d"),
                            Content = "This is simple sample article",
                            Created = DateTime.Parse("2021-01-01")
                        },
                        new Article
                        {
                            Id = new Guid("5ad823ec-e2fa-4a4a-aec8-914c7298661e"),
                            Content = "<html><head></head><body>This is an HTML article</body></html>",
                            Created = DateTime.Parse("2021-01-01")
                        }
                    );

                    context.Set<ArticleUser>().AddRange(
                        new ArticleUser
                        {
                            User_Id = new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9"),
                            ArticleId = new Guid("5ad823ec-e2fa-4a4a-aec8-914c7298661d")
                        },
                        new ArticleUser
                        {
                            User_Id = new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cda"),
                            ArticleId = new Guid("5ad823ec-e2fa-4a4a-aec8-914c7298661e")
                        },
                        new ArticleUser
                        {
                            User_Id = new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9"),
                            ArticleId = new Guid("5ad823ec-e2fa-4a4a-aec8-914c7298661f")
                        },
                        new ArticleUser
                        {
                            User_Id = new Guid("e95fa28b-1ed1-4a1b-a981-a4e608ca7cda"),
                            ArticleId = new Guid("5ad823ec-e2fa-4a4a-aec8-914c7298661f")
                        }
                    );

                    context.SaveChanges();
                }
            });
//.UseAsyncSeeding(async (context, _, cancellationToken) =>
//{
//    var testBlog = await context.Set<Blog>().FirstOrDefaultAsync(b => b.Url == "http://test.com", cancellationToken);
//    if (testBlog == null)
//    {
//        context.Set<Blog>().Add(new Blog { Url = "http://test.com" });
//        await context.SaveChangesAsync(cancellationToken);
//    }
//});
    }
}
