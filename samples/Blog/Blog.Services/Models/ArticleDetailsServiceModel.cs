namespace Blog.Services.Models
{
    using System;

    public class ArticleDetailsServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public bool IsPublic { get; set; }

        public DateTime? PublishedOn { get; set; }

        public string Author { get; set; }
    }
}
