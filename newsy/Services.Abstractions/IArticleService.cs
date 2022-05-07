using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace Services.Abstractions
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<ArticleDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<ArticleDto> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default);

        Task<ArticleDto> CreateAsync(List<Guid> authorIds, ArticleForCreationDto articleForCreationDto, CancellationToken cancellationToken = default);
    }
}