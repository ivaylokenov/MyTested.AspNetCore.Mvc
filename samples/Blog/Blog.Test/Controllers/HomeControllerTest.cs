namespace Blog.Test.Controllers
{
    using System.Collections.Generic;
    using Blog.Controllers;
    using Blog.Data.Models;
    using Data;
    using MyTested.AspNetCore.Mvc;
    using Services.Models;
    using Shouldly;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void PrivacyShouldReturnDefaultView()
            => MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Privacy())
                .ShouldReturn()
                .View();
        
        [Theory]
        [InlineData(2, true, 2)]
        [InlineData(4, true, 3)]
        [InlineData(4, false, 0)]
        public void IndexShouldReturnDefaultViewWithCorrectModel(int total, bool arePublic, int expected)
            => MyMvc
                .Controller<HomeController>()
                .WithDbContext(dbContext => dbContext
                    .WithSet<Article>(articles => articles
                        .AddRange(ArticleTestData.GetArticles(total, arePublic))))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View()
                .WithModelOfType<List<ArticleListingServiceModel>>()
                .Passing(articles => articles.Count.ShouldBe(expected));
    }
}
