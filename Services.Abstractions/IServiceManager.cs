namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IUserService UserService { get; }

        IArticleService ArticleService { get; }
    }
}
