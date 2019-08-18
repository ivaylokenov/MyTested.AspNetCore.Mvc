namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class HomeController : Controller
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)
            => this.articleService = articleService;

        public async Task<IActionResult> Index()
        {
            var articles = await this.articleService.All(pageSize: 3);

            return this.View(articles);
        }

        public IActionResult Privacy() => this.View();
    }
}
