namespace Blog.Test.Pipeline
{
    using System.Collections.Generic;
    using System.Linq;
    using Blog.Controllers;
    using Blog.Controllers.Models;
    using Blog.Data.Models;
    using Data;
    using MyTested.AspNetCore.Mvc;
    using Services;
    using Services.Models;
    using Shouldly;
    using Xunit;

    public class ArticlesPipelineTest
    {
        [Theory]
        [InlineData(8, 1, 8)]
        [InlineData(18, 1, ServicesConstants.ArticlesPerPage)]
        [InlineData(18, 2, 6)]
        public void GetAllWithPageShouldReturnDefaultViewWithCorrectModel(int total, int page, int expectedCount)
            => MyMvc
                .Pipeline()
                .ShouldMap($"/Articles/All?page={page}")
                .To<ArticlesController>(c => c.All(page))
                .Which(controller => controller
                    .WithData(ArticleTestData.GetArticles(total)))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArticleListingViewModel>()
                    .Passing(articleListing =>
                    {
                        articleListing.Articles.Count().ShouldBe(expectedCount);
                        articleListing.Total.ShouldBe(total);
                        articleListing.Page.ShouldBe(page);
                    }));

        [Fact]
        public void GetDetailsShouldReturnViewWithCorrectModelWhenPublicArticleAndAnonymousUser()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Articles/Details/1")
                .To<ArticlesController>(c => c.Details(1))
                .Which(controller => controller
                    .WithData(ArticleTestData.GetArticles(1)))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArticleDetailsServiceModel>()
                    .Passing(article => article.Id == 1));

        [Fact]
        public void GetCreateShouldShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithLocation("/Articles/Create")
                    .WithUser())
                .To<ArticlesController>(c => c.Create())
                .Which()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Test Article", "Test Article Content")]
        public void PostCreateShouldSaveArticleHaveValidModelStateAndRedirect(string title, string content)
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Articles/Create")
                    .WithFormFields(new
                    {
                        Title = title,
                        Content = content
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<ArticlesController>(c => c.Create(new ArticleFormModel
                {
                    Title = title,
                    Content = content
                }))
                .Which()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Article>(set =>
                    {
                        set.ShouldNotBeEmpty();
                        set.SingleOrDefault(a => a.Title == title).ShouldNotBeNull();
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(ControllerConstants.SuccessMessage))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArticlesController>(c => c.Mine()));

        [Fact]
        public void GetMineShouldReturnViewWithCorrectModel()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithLocation("/Articles/Mine")
                    .WithUser("Author Id 1", "Author 1"))
                .To<ArticlesController>(c => c.Mine())
                .Which(controller => controller
                    .WithData(ArticleTestData.GetArticles(2, sameUser: false)))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ArticleForUserListingServiceModel>>()
                    .Passing(articles =>
                    {
                        articles.ShouldNotBeEmpty();
                        articles.SingleOrDefault(a => a.Author == "Author 1").ShouldNotBeNull();
                    }));
    }
}
