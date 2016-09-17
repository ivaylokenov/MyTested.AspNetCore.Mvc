namespace MusicStore.Test.Routing
{
    using Microsoft.Extensions.Caching.Memory;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeRouteTest
    {
        [Fact]
        public void GetIndexShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index(
                    With.Any<MusicStoreContext>(),
                    With.Any<IMemoryCache>()));
        }

        [Fact]
        public void GetErrorShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
        }

        [Fact]
        public void GetStatusCodeShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Home/StatusCodePage")
                .To<HomeController>(c => c.StatusCodePage());
        }

        [Fact]
        public void GetAccessDeniedShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Home/AccessDenied")
                .To<HomeController>(c => c.AccessDenied());
        }
    }
}
