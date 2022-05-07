namespace Contracts
{
    public class ArticleDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string? Content { get; set; }

        public List<Guid>? AuthorIds { get; set; }
    }
}
