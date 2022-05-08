using System;

namespace Domain.Exceptions
{
    public sealed class ArticleNotFoundException : NotFoundException
    {
        public ArticleNotFoundException(Guid accountId)
            : base($"The account with the identifier {accountId} was not found.")    
        {
        }
    }
}
