namespace MusicStore.Test.Controllers
{
    using System.Collections.Generic;
    using Data;
    using Microsoft.Extensions.Caching.Memory;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnTopSellingAlbumsAndSaveThenIntoCache()
        {
            MyMvc
                .Controller<HomeController>()
                .WithOptions(options => options
                    .For<AppSettings>(settings => settings.CacheDbResults = true))
                .WithData(data => data
                    .WithSet<Album>(albums => albums
                        .AddRange(TestAlbumDataProvider.GetAlbums())))
                .Calling(c => c.Index(
                    From.Services<MusicStoreContext>(),
                    From.Services<IMemoryCache>()))
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntryOfType<List<Album>>("topselling"))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<Album>>()
                    .Passing(albums => Assert.Equal(6, albums.Count)));
        }
        
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
