namespace Domain.Entities
{
    public class Article
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string? Content { get; set; }

        public List<Guid>? AuthorIds { get; set; }
    }
}