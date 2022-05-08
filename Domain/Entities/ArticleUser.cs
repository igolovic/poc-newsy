namespace Domain.Entities
{
    public class ArticleUser
    {
        public Guid ArticleId { get; set; }
        
        public Article? Article { get; set; }

        public Guid User_Id { get; set; }

        public User? User { get; set; }
    }
}