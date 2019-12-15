namespace Blog.Test.Routing.Admin
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using ArticlesController = Web.Areas.Admin.Controllers.ArticlesController;

    public class ArticlesRouteTest
    {
        [Fact]
        public void GetAllShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Articles/All")
                .To<ArticlesController>(c => c.All());

        [Fact]
        public void GetMineShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Articles/ChangeVisibility/1")
                .To<ArticlesController>(c => c.ChangeVisibility(1));
    }
}
