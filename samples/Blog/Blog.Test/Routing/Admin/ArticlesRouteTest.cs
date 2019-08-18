namespace Blog.Test.Routing.Admin
{
    using Blog.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using ArticlesController = Web.Areas.Admin.Controllers.ArticlesController;

    public class ArticlesRouteTest
    {
        [Fact]
        public void GetAllShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Admin/Articles/All")
                    .WithUser(new[] { ControllerConstants.AdministratorRole }))
                .To<ArticlesController>(c => c.All());

        [Fact]
        public void GetMineShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Admin/Articles/ChangeVisibility/1")
                    .WithUser(new[] { ControllerConstants.AdministratorRole }))
                .To<ArticlesController>(c => c.ChangeVisibility(1));
    }
}
