namespace MusicStore.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Caching.Memory;
    using MusicStore.Areas.Admin.Controllers;
    using MusicStore.Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class StoreManagerControllerTest
    {
        [Fact]
        public void ControllerShouldHaveAuthorizeAndAreaAttributes()
        {
            MyMvc
                .Controller<StoreManagerController>()
                .ShouldHave()
                .Attributes(attr => attr
                    .RestrictingForAuthorizedRequests()
                    .SpecifyingArea("Admin"));
        }

        [Fact]
        public void IndexShouldReturnViewWithAlbums()
        {
            var itemsCount = 10;

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        var albums = CreateTestAlbums(itemsCount);
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<Album>>()
                    .Passing(model =>
                    {
                        Assert.NotNull(model);
                        Assert.Equal(itemsCount, model.Count());
                    }
                ));
        }

        [Fact]
        public void DetailsShouldHaveAlbumInMemoryCacheAndShouldReturnViewWithAlbum()
        {
            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        var albums = CreateTestAlbums(10);
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.Details(From.Services<IMemoryCache>(), 3))
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("album_3")
                        .WithSlidingExpiration(TimeSpan.FromMinutes(10))
                        .WithValueOfType<Album>()
                        .Passing(a => a.AlbumId == 3)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<Album>()
                    .Passing(model =>
                    {
                        Assert.NotNull(model);
                        Assert.Equal(3, model.AlbumId);
                    }
                ));
        }

        [Fact]
        public void DetailsShouldReturnNotFoundWhenPassingWrongAlbumId()
        {
            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        var albums = CreateTestAlbums(10);
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.Details(From.Services<IMemoryCache>(), -1))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void CreatePostShouldHaveValidateAntiForgeryTokenAndPostAttributes()
        {
            MyMvc
                .Controller<StoreManagerController>()
                .Calling(x => x.Create(
                    new Album(), 
                    From.Services<IMemoryCache>(), 
                    CancellationToken.None))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .ValidatingAntiForgeryToken()
                    .RestrictingForHttpMethod<HttpPostAttribute>());
        }

        [Fact]
        public void CreateGetShouldReturnViewWithViewBagWithGenresAndArtists()
        {
            var genres = CreateTestGenres();
            var artists = CreateTestArtists();

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(genres);
                        entities.AddRange(artists);
                    }))
                .Calling(x => x.Create())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry("GenreId", new SelectList(genres, "GenreId", "Name"))
                    .ContainingEntry("ArtistId", new SelectList(artists, "ArtistId", "Name")))
                .AndAlso()
                .ShouldReturn()
                .View(view => view.WithDefaultName())
                .AndAlso();
        }

        [Fact]
        public void CreatePostShouldHaveInvalidModelStateAndReturnDefaultViewWhenPassedEmptyAlbum()
        {
            MyMvc
                .Controller<StoreManagerController>()
                .Calling(x => x.Create(
                    new Album(), 
                    From.Services<IMemoryCache>(), 
                    CancellationToken.None))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithDefaultName()
                    .WithModelOfType<Album>());
        }

        [Fact]
        public void RemoveAlbumGetShouldReturnViewWithAlbum()
        {
            var albums = CreateTestAlbums(10);
            var albumId = 3;
            var album = albums.FirstOrDefault(x => x.AlbumId == albumId);

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.RemoveAlbum(albumId))
                .ShouldReturn()
                .View(view => view
                    .WithDefaultName()
                    .WithModelOfType<Album>()
                    .Passing(model =>
                    {
                        Assert.NotNull(model);
                        Assert.Equal(album, model);
                    }));
        }

        [Fact]
        public void RemoveAlbumGetShouldReturnNotFoundCalledWithNotExistingAlbumId()
        {
            var albums = CreateTestAlbums(10);
            var albumId = -1;

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.RemoveAlbum(albumId))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void RemoveAlbumConfirmedShouldDeleteAlbum()
        {
            var albums = CreateTestAlbums(10);
            var albumId = 3;

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.RemoveAlbumConfirmed(
                    From.Services<IMemoryCache>(), 
                    albumId, 
                    CancellationToken.None))
                .ShouldHave()
                .Data(data => data.WithEntities(context =>
                {
                    var album = context.Find<Album>(albumId);
                    Assert.Null(album);
                }))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<StoreManagerController>(c => c.Index()));
        }

        [Fact]
        public void RemoveAlbumConfirmedShouldReturnNotFoundCalledWithNotExistingAlbumId()
        {
            var albums = CreateTestAlbums(10);
            var albumId = -1;

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.RemoveAlbumConfirmed(
                    From.Services<IMemoryCache>(), 
                    albumId, 
                    CancellationToken.None))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void EditPostShouldHaveValidateAntiForgeryTokenAndPostAttributes()
        {
            MyMvc
                .Controller<StoreManagerController>()
                .Calling(x => x.Edit(
                    From.Services<IMemoryCache>(), 
                    new Album(), 
                    CancellationToken.None))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .ValidatingAntiForgeryToken()
                    .RestrictingForHttpMethod<HttpPostAttribute>());
        }

        [Fact]
        public void EditShouldReturnNotFoundCalledWithNotExistingAlbumId()
        {
            var albums = CreateTestAlbums(10);
            var albumId = -1;

            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.Edit(albumId))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void EditShouldReturnDefaultViewWithAlbum()
        {
            var albums = CreateTestAlbums(10);
            var albumId = 3;
            var album = albums.FirstOrDefault(x => x.AlbumId == albumId);
            
            MyMvc
                .Controller<StoreManagerController>()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        entities.AddRange(albums);
                    }))
                .Calling(x => x.Edit(albumId))
                .ShouldReturn()
                .View(view => view
                    .WithDefaultName()
                    .WithModelOfType<Album>()
                    .Passing(model =>
                    {
                        Assert.NotNull(model);
                        Assert.Equal(album, model);
                    }));
        }

        private static Album[] CreateTestAlbums(int itemsCount)
        {
            return Enumerable.Range(1, itemsCount).Select(n =>
                new Album
                {
                    AlbumId = n,
                    Price = 10,
                    Genre = new Genre(),
                    Artist = new Artist()
                }).ToArray();
        }

        private static Genre[] CreateTestGenres()
        {
            return Enumerable.Range(1, 10).Select(n =>
                new Genre
                {
                    GenreId = n,
                    Name = "Genre Name " + n,
                }).ToArray();
        }

        private static Artist[] CreateTestArtists()
        {
            return Enumerable.Range(1, 10).Select(n =>
                new Artist
                {
                    ArtistId = n + 1,
                    Name = "Artist Name " + n,
                }).ToArray();
        }
    }
}
