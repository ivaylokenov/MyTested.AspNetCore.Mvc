namespace Blog.Test.Pipeline
{
    using System.Collections.Generic;
    using Blog.Controllers;
    using Data;
    using Services.Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using Shouldly;

    public class HomePipelineTest
    {
        [Theory]
        [InlineData(2, true, 2)]
        [InlineData(4, true, 3)]
        [InlineData(4, false, 0)]
        public void GetIndexShouldReturnDefaultViewWithCorrectModel(int total, bool arePublic, int expected)
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                    .WithData(ArticleTestData.GetArticles(total, arePublic)))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ArticleListingServiceModel>>()
                    .Passing(articles => articles.Count.ShouldBe(expected)));

        [Fact]
        public void GetPrivacyShouldReturnDefaultView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Privacy")
                .To<HomeController>(c => c.Privacy())
                .Which()
                .ShouldReturn()
                .View();
    }
}
