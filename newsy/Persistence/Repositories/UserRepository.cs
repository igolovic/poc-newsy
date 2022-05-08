using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public UserRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default) =>
            //await _dbContext.Users.Include(item => item.Accounts).ToListAsync(cancellationToken);
            await _dbContext.Users.Include(item => item.ArticleUser).ToListAsync(cancellationToken);

        public async Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
            //await _dbContext.Users.Include(item => item.Accounts).FirstOrDefaultAsync(item => item.Id == userId, cancellationToken);
            await _dbContext.Users.Include(item => item.ArticleUser).FirstOrDefaultAsync(item => item.Id == userId, cancellationToken);
    }
}
