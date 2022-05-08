namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }

        IArticleRepository ArticleRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
