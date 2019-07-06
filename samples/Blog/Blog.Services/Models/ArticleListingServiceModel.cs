namespace Blog.Services.Models
{
    using System;

    public class ArticleListingServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedOn { get; set; }

        public string Author { get; set; }
    }
}
