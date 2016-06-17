namespace MusicStore.Test.Routes
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
                .Routes()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index(
                    With.Any<MusicStoreContext>(),
                    With.Any<IMemoryCache>()));
        }

        [Fact]
        public void GetErrorShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
        }

        [Fact]
        public void GetStatusCodeShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/StatusCodePage")
                .To<HomeController>(c => c.StatusCodePage());
        }

        [Fact]
        public void GetAccessDeniedShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/AccessDenied")
                .To<HomeController>(c => c.AccessDenied());
        }
    }
}
