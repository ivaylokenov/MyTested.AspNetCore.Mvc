namespace Blog.Test.Controllers
{
    using System.Collections.Generic;
    using Blog.Controllers;
    using Blog.Controllers.Models;
    using Blog.Data.Models;
    using Data;
    using MyTested.AspNetCore.Mvc;
    using Services;
    using Services.Models;
    using System.Linq;
    using Xunit;
    using Shouldly;

    public class ArticlesControllerTest
    {
        [Theory]
        [InlineData(8, 1, 8)]
        [InlineData(18, 1, ServicesConstants.ArticlesPerPage)]
        [InlineData(18, 2, 6)]
        public void AllShouldReturnDefaultViewWithCorrectModel(int total, int page, int expectedCount)
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithData(ArticleTestData.GetArticles(total)))
                .Calling(c => c.All(page))
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
        public void DetailsShouldReturnNotFoundWhenInvalidArticleId()
            => MyController<ArticlesController>
                .Calling(c => c.Details(int.MaxValue))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void DetailsShouldReturnNotFoundWhenNonPublicArticleAndAnonymousUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithData(ArticleTestData.GetArticles(1, isPublic: false)))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void DetailsShouldReturnViewWithCorrectModelWhenPublicArticleAndAnonymousUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArticleDetailsServiceModel>()
                    .Passing(article => article.Id == 1));

        [Fact]
        public void DetailsShouldReturnNotFoundWhenNonPublicArticleAndNonAdministratorNonAuthorUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser("NonAuthor")
                    .WithData(ArticleTestData.GetArticles(1, isPublic: false)))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void DetailsShouldReturnViewWithCorrectModelWhenPublicArticleAndNonAdministratorNonAuthorUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser("NonAuthor")
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArticleDetailsServiceModel>()
                    .Passing(article => article.Id == 1));

        [Theory]
        [InlineData(true, TestUser.Username, null)]
        [InlineData(false, TestUser.Username, null)]
        [InlineData(true, "Administrator", ControllerConstants.AdministratorRole)]
        [InlineData(false, "Administrator", ControllerConstants.AdministratorRole)]
        public void DetailsShouldReturnViewWithCorrectModelWhenCorrectUser(
            bool isPublic,
            string username,
            string role)
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(ArticleTestData.GetArticles(1, isPublic)))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArticleDetailsServiceModel>()
                    .Passing(article => article.Id == 1));
        
        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView()
            => MyController<ArticlesController>
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Get)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ArticlesController>
                .Calling(c => c.Create(With.Empty<ArticleFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<ArticlesController>
                .Calling(c => c.Create(With.Default<ArticleFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<ArticleFormModel>());

        [Theory]
        [InlineData("Article Title", "Article Content")]
        public void CreatePostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModel(string title, string content)
            => MyController<ArticlesController>
                .Calling(c => c.Create(new ArticleFormModel
                {
                    Title = title,
                    Content = content
                }))
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
        public void EditGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsers()
            => MyController<ArticlesController>
                .Calling(c => c.Edit(With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Get)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditGetShouldReturnNotFoundWhenInvalidId()
            => MyController<ArticlesController>
                .Calling(c => c.Edit(With.Any<int>()))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void EditGetShouldReturnNotFoundWhenNonAuthorUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser("NonAuthor")
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .NotFound();

        [Theory]
        [InlineData(1, TestUser.Username, null)]
        [InlineData(1, "Administrator", ControllerConstants.AdministratorRole)]
        public void EditGetShouldReturnViewWithCorrectModelWhenCorrectUser(int articleId, string username, string role)
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(ArticleTestData.GetArticles(articleId)))
                .Calling(c => c.Edit(articleId))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArticleFormModel>()
                    .Passing(article => article.Title == $"Article {articleId}"));
    
        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ArticlesController>
                .Calling(c => c.Edit(
                    With.Empty<int>(), 
                    With.Empty<ArticleFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
        
        [Fact]
        public void EditPostShouldReturnNotFoundWhenInvalidId()
            => MyController<ArticlesController>
                .Calling(c => c.Edit(
                    With.Any<int>(),
                    With.Any<ArticleFormModel>()))
                .ShouldReturn()
                .NotFound();
        
        [Fact]
        public void EditPostShouldReturnNotFoundWhenNonAuthorUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(user => user.WithIdentifier("NonAuthor"))
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Edit(1, With.Empty<ArticleFormModel>()))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Edit(1, With.Default<ArticleFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<ArticleFormModel>());

        [Theory]
        [InlineData(1, "Article Title", "Article Content", TestUser.Username, null)]
        [InlineData(1, "Article Title", "Article Content", "Administrator", ControllerConstants.AdministratorRole)]
        public void EditPostShouldSaveArticleSetTempDataMessageAndRedirectWhenValidModelState(
            int articleId,
            string title, 
            string content, 
            string username, 
            string role)
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Edit(articleId, new ArticleFormModel
                {
                    Title = $"Edit {title}",
                    Content = $"Edit {content}"
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Article>(set =>
                    {
                        set.ShouldNotBeEmpty();

                        var article = set.SingleOrDefault(a => a.Id == articleId);

                        article.ShouldNotBeNull();
                        article.Title.ShouldBe($"Edit {title}");
                        article.Content.ShouldBe($"Edit {content}");
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
        public void DeleteShouldHaveRestrictionsForAuthorizedUsers()
            => MyController<ArticlesController>
                .Calling(c => c.Delete(With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());
        
        [Fact]
        public void DeleteShouldReturnNotFoundWhenInvalidId()
            => MyController<ArticlesController>
                .Calling(c => c.Delete(With.Any<int>()))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void DeleteShouldReturnNotFoundWhenNonAuthorUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(user => user.WithIdentifier("NonAuthor"))
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .NotFound();
    
        [Theory]
        [InlineData(1, TestUser.Username, null)]
        [InlineData(1, "Administrator", ControllerConstants.AdministratorRole)]
        public void DeleteShouldReturnViewWithCorrectModelWhenCorrectUser(int articleId, string username, string role)
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(ArticleTestData.GetArticles(articleId)))
                .Calling(c => c.Delete(articleId))
                .ShouldReturn()
                .View(articleId);

        [Fact]
        public void ConfirmDeleteShouldHaveRestrictionsForAuthorizedUsers()
            => MyController<ArticlesController>
                .Calling(c => c.ConfirmDelete(With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void ConfirmDeleteShouldReturnNotFoundWhenInvalidId()
            => MyController<ArticlesController>
                .Calling(c => c.ConfirmDelete(With.Any<int>()))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void ConfirmDeleteShouldReturnNotFoundWhenNonAuthorUser()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(user => user.WithIdentifier("NonAuthor"))
                    .WithData(ArticleTestData.GetArticles(1)))
                .Calling(c => c.ConfirmDelete(1))
                .ShouldReturn()
                .NotFound();

        [Theory]
        [InlineData(1, TestUser.Username, null)]
        [InlineData(1, "Administrator", ControllerConstants.AdministratorRole)]
        public void ConfirmDeleteShouldDeleteArticleSetTempDataMessageAndRedirectWhenValidId(
            int articleId, 
            string username, 
            string role)
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(ArticleTestData.GetArticles(articleId)))
                .Calling(c => c.ConfirmDelete(articleId))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Article>(set => set.ShouldBeEmpty()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(ControllerConstants.SuccessMessage))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArticlesController>(c => c.Mine()));

        [Fact]
        public void MineShouldHaveRestrictionsForAuthorizedUsers()
            => MyController<ArticlesController>
                .Calling(c => c.Mine())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());
        
        [Fact]
        public void MineShouldReturnViewWithCorrectModel()
            => MyController<ArticlesController>
                .Instance(instance => instance
                    .WithUser("Author Id 1", "Author 1")
                    .WithData(ArticleTestData.GetArticles(2, sameUser: false)))
                .Calling(c => c.Mine())
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
