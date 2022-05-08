namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public IEnumerable<ArticleUser>? ArticleUser { get; set; }
    }
}