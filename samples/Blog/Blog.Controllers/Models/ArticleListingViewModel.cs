namespace Blog.Controllers.Models
{
    using System.Collections.Generic;
    using Services.Models;

    public class ArticleListingViewModel
    {
        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }
    }
}
