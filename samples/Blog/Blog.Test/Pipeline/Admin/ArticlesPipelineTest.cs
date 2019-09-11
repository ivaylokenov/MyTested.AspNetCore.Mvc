namespace Blog.Test.Pipeline.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blog.Controllers;
    using Blog.Data.Models;
    using Data;
    using MyTested.AspNetCore.Mvc;
    using Services.Models;
    using Shouldly;
    using Xunit;

    using ArticlesController = Web.Areas.Admin.Controllers.ArticlesController;

    public class ArticlesPipelineTest
    {
        [Fact]
        public void GetAllShouldReturnViewWithAllArticles()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithLocation("/Admin/Articles/All")
                    .WithUser(new[] { ControllerConstants.AdministratorRole }))
                .To<ArticlesController>(c => c.All())
                .Which(controller => controller
                    .WithData(ArticleTestData.GetArticles(1, isPublic: false)))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ArticleNonPublicListingServiceModel>>()
                    .Passing(articles => articles.ShouldNotBeEmpty()));

        [Fact]
        public void GetMineShouldChangeArticleAndRedirectToAll()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithLocation("/Admin/Articles/ChangeVisibility/1")
                    .WithUser(new[] { ControllerConstants.AdministratorRole }))
                .To<ArticlesController>(c => c.ChangeVisibility(1))
                .Which(controller => controller
                    .WithData(ArticleTestData.GetArticles(1, isPublic: false)))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Article>(set =>
                    {
                        var article = set.FirstOrDefault(a => a.IsPublic);

                        article.ShouldNotBeNull();
                        article.PublishedOn.ShouldNotBeNull();
                        article.PublishedOn.ShouldBe(new DateTime(1, 1, 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArticlesController>(c => c.All()));
    }
}
