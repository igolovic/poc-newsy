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
            //await _dbContext.Users.Include(x => x.Accounts).ToListAsync(cancellationToken);
            await _dbContext.Users.ToListAsync(cancellationToken);

        public async Task<User> GetByIdAsync(Guid ownerId, CancellationToken cancellationToken = default) =>
            //await _dbContext.Users.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == ownerId, cancellationToken);
            await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == ownerId, cancellationToken);
    }
}
