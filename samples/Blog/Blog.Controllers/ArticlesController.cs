namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticlesController(
            IArticleService articleService,
            IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> All([FromQuery]int page = 1) 
            => this.View(new ArticleListingViewModel
            {
                Articles = await this.articleService.All(page),
                Total = await this.articleService.Total(),
                Page = page
            });

        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articleService.Details(id);

            if (article == null)
            {
                return this.NotFound();
            }

            if (!this.User.IsAdministrator() 
                && !article.IsPublic 
                && article.Author != this.User.Identity.Name)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ArticleFormModel article)
        {
            if (this.ModelState.IsValid)
            {
                await this.articleService.Add(article.Title, article.Content, this.User.GetId());

                this.TempData.Add(ControllerConstants.SuccessMessage, "Article created successfully it is waiting for approval!");

                return this.RedirectToAction(nameof(this.Mine));
            }

            return this.View(article);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await this.articleService.Details(id);

            if (article == null || (article.Author != this.User.Identity.Name && !this.User.IsAdministrator()))
            {
                return this.NotFound();
            }

            var articleEdit = this.mapper.Map<ArticleFormModel>(article);

            return this.View(articleEdit);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ArticleFormModel article)
        {
            if (!await this.articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }
            
            if (this.ModelState.IsValid)
            {
                await this.articleService.Edit(id, article.Title, article.Content);

                this.TempData.Add(ControllerConstants.SuccessMessage, "Article edited successfully and is waiting for approval!");

                return this.RedirectToAction(nameof(this.Mine));
            }

            return this.View(article);
        }
        
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }

            return this.View(id);
        }
        
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (!await this.articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }

            await this.articleService.Delete(id);
            
            this.TempData.Add(ControllerConstants.SuccessMessage, "Article deleted successfully!");

            return this.RedirectToAction(nameof(this.Mine));
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            var articles = await this.articleService.ByUser(this.User.GetId());

            return this.View(articles);
        }
    }
}
