namespace Blog.Services.Models
{
    public class ArticleNonPublicListingServiceModel : ArticleListingServiceModel
    {
        public bool IsPublic { get; set; }
    }
}
