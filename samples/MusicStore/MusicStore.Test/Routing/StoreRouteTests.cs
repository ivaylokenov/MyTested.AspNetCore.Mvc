namespace MusicStore.Test.Routing
{
    using Microsoft.Extensions.Caching.Memory;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class StoreRouteTests
    {
        [Fact]
        public void GetIndexShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Store/Index")
                .To<StoreController>(c => c.Index());
        }

        [Fact]
        public void GetBrowseShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Store/Browse?genre=Disco")
                .To<StoreController>(c => c.Browse("Disco"));
        }

        [Fact]
        public void GetDetailsShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/Store/Details/1")
                .To<StoreController>(c => c.Details(With.Any<IMemoryCache>(), 1)); ;
        }
    }
}
