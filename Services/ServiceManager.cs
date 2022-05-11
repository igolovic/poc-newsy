using System;

using Domain.Repositories;

using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IArticleService> _lazyArticleService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            _lazyArticleService = new Lazy<IArticleService>(() => new ArticleService(repositoryManager));
        }

        public IUserService UserService => _lazyUserService.Value;

        public IArticleService ArticleService => _lazyArticleService.Value;
    }
}
