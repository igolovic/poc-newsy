using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<Article>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Article> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default);

        Guid Insert(Article account);
    }
}
