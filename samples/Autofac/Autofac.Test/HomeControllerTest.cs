namespace Autofac.Test
{
    using MyTested.AspNetCore.Mvc;
    using Web.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void CustomRouteShouldMatchCorrectController()
            => MyRouting
                .Configuration()
                .ShouldMap("/Index")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithName(nameof(HomeController.Index))
                    .WithModelOfType<string>()
                    .Passing(model => model == "1/1/2020 Test Data"));

        [Fact]
        public void RedirectShouldRedirectToIndex()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.RedirectToIndex())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
