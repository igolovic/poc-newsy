namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IUserService UserService { get; }

        IArticleService AccountService { get; }
    }
}
