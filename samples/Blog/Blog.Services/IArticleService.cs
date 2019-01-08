namespace Blog.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleService
    {
        Task<IEnumerable<ArticleListingServiceModel>> All(
            int page = 1, 
            int pageSize = ServicesConstants.ArticlesPerPage, 
            bool publicOnly = true);

        Task<IEnumerable<TModel>> All<TModel>(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            where TModel : class;

        Task<IEnumerable<ArticleForUserListingServiceModel>> ByUser(string userId);

        Task<bool> IsByUser(int id, string userId);

        Task<ArticleDetailsServiceModel> Details(int id);

        Task<int> Total();

        Task<int> Add(string title, string content, string userId);
        
        Task Edit(int id, string title, string content);

        Task Delete(int id);

        Task ChangeVisibility(int id);
    }
}
