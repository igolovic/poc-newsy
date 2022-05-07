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
            await _dbContext.Articles.ToListAsync(cancellationToken);

        public async Task<IEnumerable<Article>> GetAllByUserIdAsync(Guid ownerId, CancellationToken cancellationToken = default) =>
            await _dbContext.Articles.Where(x => x.Id == ownerId).ToListAsync(cancellationToken);

        public async Task<Article> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default) =>
            await _dbContext.Articles.FirstOrDefaultAsync(x => x.Id == accountId, cancellationToken);

        public void Insert(Article account) => _dbContext.Articles.Add(account);

        public void Remove(Article account) => _dbContext.Articles.Remove(account);
    }
}
