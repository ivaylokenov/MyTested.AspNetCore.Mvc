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
        public void IndexShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index(
                    With.Any<MusicStoreContext>(),
                    With.Any<IMemoryCache>()));
        }

        [Fact]
        public void ErrorShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
        }
    }
}
