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

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = GetData(Guid.Empty);
            return result;
        }

        public async Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var result = GetData(userId);
            return result.FirstOrDefault();
        }

        private IEnumerable<User> GetData(Guid userId)
        {
            var resAsync = (from u in _dbContext.Users
                            join au in _dbContext.ArticleUser on u.Id equals au.User_Id into g
                            from au2 in g.DefaultIfEmpty()
                            join a in _dbContext.Articles on au2.ArticleId equals a.Id into g2
                            from a2 in g2.DefaultIfEmpty()
                            where (userId == Guid.Empty)
                                || (userId != Guid.Empty && au2.User_Id == userId)
                            select new
                            {
                                Id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                UserName = u.UserName,
                                ArticleId = au2 != null ? au2.ArticleId : Guid.Empty,
                                User_Id = au2 != null ? au2.User_Id : Guid.Empty,
                            }).ToList();

            var res = resAsync;

            var resGrp = from i in res
                         group i by i.Id into g
                         let u = g.First()
                         select new User
                         {
                             Id = u.Id,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             UserName = u.UserName,
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
    }
}
