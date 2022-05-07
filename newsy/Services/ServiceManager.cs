using System;

using Domain.Repositories;

using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _lazyOwnerService;
        private readonly Lazy<IArticleService> _lazyAccountService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyOwnerService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            _lazyAccountService = new Lazy<IArticleService>(() => new ArticleService(repositoryManager));
        }

        public IUserService UserService => _lazyOwnerService.Value;

        public IArticleService AccountService => _lazyAccountService.Value;
    }
}
