namespace Blog.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Blog.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models;

    [Area(ControllerConstants.AdministratorArea)]
    [Authorize(Roles = ControllerConstants.AdministratorRole)]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService)
            => this.articleService = articleService;

        public async Task<IActionResult> All()
        {
            var articles = await this.articleService
                .All<ArticleNonPublicListingServiceModel>(pageSize: int.MaxValue, publicOnly: false);

            return this.View(articles);
        }
        
        public async Task<IActionResult> ChangeVisibility(int id)
        {
            await this.articleService.ChangeVisibility(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
