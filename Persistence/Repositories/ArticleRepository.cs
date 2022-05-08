using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class ArticleRepository : IArticleRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public ArticleRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Article>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // This doesn't work because NPGSQL is translating wrongly trivial query (adds non-existing column UserId to SELECT list!)
            //return await _dbContext.Articles.Include(item => item.ArticleUser).ThenInclude(item => item.User).ToListAsync(cancellationToken);

            var result = GetData(Guid.Empty, Guid.Empty);
            return result;

        }

        public async Task<IEnumerable<Article>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var result = GetData(Guid.Empty, userId);
            return result;
        }

        public async Task<Article> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default)
        {
            var result = GetData(articleId, Guid.Empty);
            return result.FirstOrDefault();
        }

        private IEnumerable<Article> GetData(Guid articleId, Guid userId)
        {
            var resAsync = (from a in _dbContext.Articles
                            join au in _dbContext.ArticleUser on a.Id equals au.ArticleId into g
                            from au2 in g.DefaultIfEmpty()
                            where (userId == Guid.Empty && articleId == Guid.Empty)
                                || (userId != Guid.Empty && au2.User_Id == userId)
                                || (articleId != Guid.Empty && a.Id == articleId)
                            select new
                            {
                                Id = a.Id,
                                Content = a.Content,
                                Created = a.Created,
                                ArticleId = au2 != null ? au2.ArticleId : Guid.Empty,
                                User_Id = au2 != null ? au2.User_Id : Guid.Empty,
                            }).ToList();

            var res = resAsync;

            var resGrp = from i in res
                         group i by i.Id into g
                         let a = g.First()
                         select new Article
                         {
                             Id = a.Id,
                             Content = a.Content,
                             Created = a.Created,
                             ArticleUser = g
                                .Select(item => new ArticleUser
                                {
                                    ArticleId = item.ArticleId,
                                    User_Id = item.User_Id
                                })
                                .ToList()
                         };

            return resGrp;
        }

        public Guid Insert(Article article)
        {
            // It gets even more erratic - this too doesn't work because NPGSQL is translating
            // wrongly trivial query (adds non-existing column UserId to INSERT column list!)
            //var insertedArticle = _dbContext.Articles.Add(article);

            var insertedArticleId = Guid.NewGuid();
            _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO public.""Article"" (""Id"", ""Content"", ""Created"") VALUES ({0}, {1}, {2})", insertedArticleId, article.Content, article.Created);
            foreach (var au in article.ArticleUser)
            {
                _dbContext.Database.ExecuteSqlRaw(@"INSERT INTO public.""ArticleUser""(""User_Id"", ""ArticleId"") VALUES ({0}, {1})", au.User_Id, insertedArticleId);
            }

            return insertedArticleId;
        }
    }
}
