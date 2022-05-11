using System;

namespace Domain.Exceptions
{
    public sealed class ArticleNotFoundException : NotFoundException
    {
        public ArticleNotFoundException(Guid articleId)
            : base($"The article with the identifier {articleId} was not found.")    
        {
        }
    }
}
