namespace Autofac.NoContainerBuilder.Test
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
                .View(nameof(HomeController.Index))
                .WithModel("1/1/2020 Test Data");

        [Fact]
        public void RedirectShouldRedirectToIndex()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.RedirectToIndex())
                .ShouldReturn()
                .Redirect()
                .To<HomeController>(c => c.Index());
    }
}
