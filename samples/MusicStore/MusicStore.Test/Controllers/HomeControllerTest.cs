namespace MusicStore.Test.Controllers
{
    using MusicStore.Controllers;
    using MyTested.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnCorrectView()
        {
            MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View("~/Views/Shared/Error.cshtml");
        }

        [Fact]
        public void StatusCodePageShouldReturnCorrectView()
        {
            MyMvc
                .Controller<HomeController>()
                .Calling(c => c.StatusCodePage())
                .ShouldReturn()
                .View("~/Views/Shared/StatusCodePage.cshtml");
        }

        [Fact]
        public void AccessDeniedShouldReturnCorrectView()
        {
            MyMvc
                .Controller<HomeController>()
                .Calling(c => c.AccessDenied())
                .ShouldReturn()
                .View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
