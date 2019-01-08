namespace Blog.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blog.Data.Models;

    public static class ArticleTestData
    {
        public static List<Article> GetArticles(int count, bool isPublic)
            => Enumerable
                .Range(1, count)
                .Select(i => new Article
                {
                    Id = i,
                    Title = $"Article {i}",
                    Content = $"Article Content {i}",
                    IsPublic = isPublic,
                    PublishedOn = isPublic ? new DateTime(2019, 1, 1) : default(DateTime?)
                })
                .ToList();
    }
}
