using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Contracts;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

using Mapster;

using Services.Abstractions;

namespace Services
{
    internal sealed class ArticleService : IArticleService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ArticleService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<IEnumerable<ArticleDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var articles = await _repositoryManager.ArticleRepository.GetAllAsync(cancellationToken);

            var articlesDto = articles.Adapt<IEnumerable<ArticleDto>>();

            return articlesDto;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var articles = await _repositoryManager.ArticleRepository.GetAllByUserIdAsync(userId, cancellationToken);

            var articlesDto = articles.Adapt<IEnumerable<ArticleDto>>();

            return articlesDto;
        }

        public async Task<ArticleDto> GetByIdAsync(Guid articleId, CancellationToken cancellationToken)
        {
            var article = await _repositoryManager.ArticleRepository.GetByIdAsync(articleId, cancellationToken);

            if (article is null)
            {
                throw new UserNotFoundException(articleId);
            }

            var articleDto = article.Adapt<ArticleDto>();

            return articleDto;
        }

        public async Task<ArticleDto> CreateAsync(ArticleForCreationDto articleForCreationDto, CancellationToken cancellationToken = default)
        {
            var article = articleForCreationDto.Adapt<Article>();

            var newArticleId = _repositoryManager.ArticleRepository.Insert(article);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            var articleDto = article.Adapt<ArticleDto>();

            // Fix ID - due to the NPGSQL problem with fictional column usual ORM mechanism cannot be used here
            articleDto.Id = newArticleId;
            List<ArticleUserDto> aarticleUserDto = articleDto.ArticleUser.ToList();
            for (int i = 0; i < aarticleUserDto.Count; i++)
                aarticleUserDto[i].ArticleId = newArticleId;

            articleDto.ArticleUser = aarticleUserDto;

            return articleDto;
        }
    }
}
