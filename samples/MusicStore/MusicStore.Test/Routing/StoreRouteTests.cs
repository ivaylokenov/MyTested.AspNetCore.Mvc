namespace MusicStore.Test.Routing
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
                .Routing()
                .ShouldMap("/Store/Index")
                .To<StoreController>(c => c.Index());
        }

        [Fact]
        public void BrowseShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Store/Browse?genre=Disco")
                .To<StoreController>(c => c.Browse("Disco"));
        }

        [Fact]
        public void DetailsShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Store/Details/1")
                .To<StoreController>(c => c.Details(With.Any<IMemoryCache>(), 1)); ;
        }
    }
}
