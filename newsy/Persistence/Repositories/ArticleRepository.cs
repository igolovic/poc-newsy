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

        public async Task<IEnumerable<Article>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.Articles.Include(item => item.ArticleUser).ToListAsync(cancellationToken);

        public async Task<IEnumerable<Article>> GetAllByUserIdAsync(Guid ownerId, CancellationToken cancellationToken = default) =>
            await _dbContext.Articles.Include(item => item.ArticleUser).Where(item => item.Id == ownerId).ToListAsync(cancellationToken);

        public async Task<Article> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default) =>
            await _dbContext.Articles.Include(item => item.ArticleUser).FirstOrDefaultAsync(item => item.Id == articleId, cancellationToken);

        public void Insert(Article article) => _dbContext.Articles.Add(article);
    }
}
