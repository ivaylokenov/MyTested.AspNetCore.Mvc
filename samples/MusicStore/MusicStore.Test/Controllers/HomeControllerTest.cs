namespace MusicStore.Test.Controllers
{
    using Microsoft.Extensions.Caching.Memory;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
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
                .WithDbContext(dbContext => dbContext
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

        private class TestAlbumDataProvider
        {
            public static Album[] GetAlbums()
            {
                var generes = Enumerable.Range(1, 10).Select(n =>
                    new Genre
                    {
                        GenreId = n,
                        Name = "Genre Name " + n,
                    }).ToArray();

                var artists = Enumerable.Range(1, 10).Select(n =>
                    new Artist
                    {
                        ArtistId = n + 1,
                        Name = "Artist Name " + n,
                    }).ToArray();

                var albums = Enumerable.Range(1, 10).Select(n =>
                    new Album
                    {
                        Artist = artists[n - 1],
                        ArtistId = n,
                        Genre = generes[n - 1],
                        GenreId = n,
                    }).ToArray();

                return albums;
            }
        }
    }
}
