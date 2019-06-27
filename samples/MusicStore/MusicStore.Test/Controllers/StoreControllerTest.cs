namespace MusicStore.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class StoreControllerTest
    {
        [Fact]
        public void IndexShouldGenerateViewWithGenres()
        {
            MyMvc
                .Controller<StoreController>()
                .WithData(db => db
                    .WithEntities(entities => CreateTestGenres(
                        numberOfGenres: 10,
                        numberOfAlbums: 1,
                        dbContext: entities)))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<Genre>>()
                    .Passing(m => m.Count == 10));
        }

        [Fact]
        public void BrowseShouldReturnNotFoundWithNoGenreData()
        {
            MyMvc
                .Controller<StoreController>()
                .Calling(c => c.Browse(string.Empty))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void BrowseShouldReturnViewGenre()
        {
            var genre = "Genre 1";

            MyMvc
                .Controller<StoreController>()
                .WithData(db => db
                    .WithEntities(entities => CreateTestGenres(
                        numberOfGenres: 3,
                        numberOfAlbums: 3,
                        dbContext: entities)))
                .Calling(c => c.Browse(genre))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<Genre>()
                    .Passing(model =>
                    {
                        Assert.Equal(genre, model.Name);
                        Assert.NotNull(model.Albums);
                        Assert.Equal(3, model.Albums.Count);
                    }));
        }

        [Fact]
        public void DetailsShouldReturnNotFoundWithNoData()
        {
            MyMvc
                .Controller<StoreController>()
                .Calling(c => c.Details(From.Services<IMemoryCache>(), int.MinValue))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void DetailsShouldReturnAlbumDetails()
        {
            Genre[] genres = null;
            var albumId = 1;

            MyMvc
                .Controller<StoreController>()
                .WithOptions(options => options
                    .For<AppSettings>(settings => settings.CacheDbResults = true))
                .WithData(db => db
                    .WithEntities(entities => genres = CreateTestGenres(
                        numberOfGenres: 3,
                        numberOfAlbums: 3,
                        dbContext: entities)))
                .Calling(c => c.Details(From.Services<IMemoryCache>(), 1))
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("album_1")
                        .WithSlidingExpiration(TimeSpan.FromMinutes(10))
                        .WithValueOfType<Album>()
                        .Passing(a => a.AlbumId == 1)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<Album>()
                    .Passing(model =>
                    {
                        Assert.NotNull(model.Genre);
                        var genre = genres.SingleOrDefault(g => g.GenreId == model.GenreId);
                        Assert.NotNull(genre);
                        Assert.NotNull(genre.Albums.SingleOrDefault(a => a.AlbumId == albumId));
                        Assert.NotNull(model.Artist);
                    }));
        }

        private static Genre[] CreateTestGenres(int numberOfGenres, int numberOfAlbums, DbContext dbContext)
        {
            var albums = Enumerable.Range(1, numberOfAlbums * numberOfGenres).Select(n =>
                  new Album
                  {
                      AlbumId = n,
                      Artist = new Artist
                      {
                          ArtistId = n,
                          Name = "Artist " + n
                      }
                  }).ToList();

            var genres = Enumerable.Range(1, numberOfGenres).Select(n =>
                 new Genre
                 {
                     Albums = albums.Where(i => i.AlbumId % numberOfGenres == n - 1).ToList(),
                     GenreId = n,
                     Name = "Genre " + n
                 });

            dbContext.AddRange(albums);
            dbContext.AddRange(genres);
            dbContext.SaveChanges();

            return genres.ToArray();
        }
    }
}
