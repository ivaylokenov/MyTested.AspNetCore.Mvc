namespace MusicStore.Test.Pipeline
{
    using System.Collections.Generic;
    using Data;
    using Microsoft.Extensions.Caching.Memory;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomePipelineTest
    {
        [Fact]
        public void GetIndexShouldReturnTopSellingAlbumsAndSaveThenIntoCache()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index(
                    From.Services<MusicStoreContext>(),
                    From.Services<IMemoryCache>()))
                .Which()
                .WithOptions(options => options
                    .For<AppSettings>(settings => settings.CacheDbResults = true))
                .WithData(data => data
                    .WithSet<Album>(albums => albums
                        .AddRange(TestAlbumDataProvider.GetAlbums())))
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntryOfType<List<Album>>("topselling"))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<Album>>()
                    .Passing(albums => Assert.Equal(6, albums.Count))); ;
        }

        [Fact]
        public void GetErrorShouldReturnCorrectView()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View("~/Views/Shared/Error.cshtml");
        }
    }
}
