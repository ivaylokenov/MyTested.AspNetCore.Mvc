namespace Blog.Test.Routing
{
    using Blog.Controllers;
    using Blog.Controllers.Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ArticlesRouteTest
    {
        [Fact]
        public void GetAllShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Articles/All")
                .To<ArticlesController>(c => c.All(With.No<int>()));

        [Fact]
        public void GetAllWithPageShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Articles/All?page=1")
                .To<ArticlesController>(c => c.All(1));

        [Fact]
        public void GetDetailsShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Articles/Details/1")
                .To<ArticlesController>(c => c.Details(1));

        [Fact]
        public void GetCreateShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Articles/Create")
                    .WithUser())
                .To<ArticlesController>(c => c.Create());

        [Theory]
        [InlineData("Test Article", "Test Article Content")]
        public void PostCreateShouldBeRoutedCorrectly(string title, string content)
            => MyRouting
                .Configuration()
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
                .AndAlso()
                .ToValidModelState();

        [Fact]
        public void GetEditShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Articles/Edit/1")
                    .WithUser())
                .To<ArticlesController>(c => c.Edit(1));

        [Theory]
        [InlineData("Test Article", "Test Article Content")]
        public void PostEditShouldBeRoutedCorrectly(string title, string content)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Articles/Edit/1")
                    .WithFormFields(new
                    {
                        Title = title,
                        Content = content
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<ArticlesController>(c => c.Edit(1, new ArticleFormModel
                {
                    Title = title,
                    Content = content
                }))
                .AndAlso()
                .ToValidModelState();

        [Fact]
        public void GetDeleteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Articles/Delete/1")
                    .WithUser())
                .To<ArticlesController>(c => c.Delete(1));

        [Fact]
        public void GetConfirmDeleteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Articles/ConfirmDelete/1")
                    .WithUser())
                .To<ArticlesController>(c => c.ConfirmDelete(1));
        
        [Fact]
        public void GetMineShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Articles/Mine")
                    .WithUser())
                .To<ArticlesController>(c => c.Mine());
    }
}
