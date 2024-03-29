﻿using System;

namespace Contracts
{
    public class ArticleForCreationDto
    {
        public DateTime Created { get; set; }

        public string? Content { get; set; }
        
        public IEnumerable<ArticleUserDto>? ArticleUser { get; set; }
    }
}