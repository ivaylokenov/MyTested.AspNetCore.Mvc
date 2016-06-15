namespace MusicStore.Test.Routes
{
    using Microsoft.Extensions.Caching.Memory;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class StoreRouteTests
    {
        [Fact]
        public void IndexShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Store/Index")
                .To<StoreController>(c => c.Index());
        }

        [Fact]
        public void BrowseShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Store/Browse?genre=Disco")
                .To<StoreController>(c => c.Browse("Disco"));
        }

        [Fact]
        public void DetailsShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Store/Details/1")
                .To<StoreController>(c => c.Details(With.Any<IMemoryCache>(), 1)); ;
        }
    }
}
