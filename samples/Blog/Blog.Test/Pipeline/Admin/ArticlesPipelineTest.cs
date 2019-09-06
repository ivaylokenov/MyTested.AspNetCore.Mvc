namespace Blog.Test.Pipeline.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blog.Controllers;
    using Blog.Data.Models;
    using Data;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Services.Models;
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
                    .Passing(articles => articles
                        .Should()
                        .NotBeEmpty()));

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
                    .WithSet<Article>(set => set
                        .FirstOrDefault(a => a.IsPublic)
                        .Should()
                        .NotBeNull()
                        .And
                        .Subject
                        .As<Article>()
                        .PublishedOn
                        .Should()
                        .NotBeNull()
                        .And
                        .Should()
                        .Be(new DateTime(1, 1, 1))))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ArticlesController>(c => c.All()));
    }
}
