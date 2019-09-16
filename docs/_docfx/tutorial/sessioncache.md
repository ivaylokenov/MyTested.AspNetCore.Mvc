# Session & Cache

In this section we will cover two of the most commonly used modules for data persistence between different requests - session and cache.

## Session

To use the built-in session capabilities of My Tested ASP.NET Core MVC, we need to add **"MyTested.AspNetCore.Mvc.Session"** as a dependency:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Attributes" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Options" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.Session package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Session" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

Adding this package will replace the default session services with scoped mocks, which are cleared at the beginning of each test. It's quite easy to test with them. Let's see! :)

We will test the **"AddToCart"** action in the **"ShoppingCartController"**. If you examine the method, you will see it calls **"ShoppingCart.GetCart"**, which creates a session entry containing the cart ID:

```c#
// code skipped for brevity

var cartId = context.Session.GetString("Session");

if (cartId == null)
{
    cartId = Guid.NewGuid().ToString();
    context.Session.SetString("Session", cartId);
}

return cartId;

// code skipped for brevity
```

Let's assert that if the session is initially empty, an entry with **"Session"** key should be added after the action invocation. Go to the **"ShoppingCartControllerTest"** class and insert the following test:

```c#
[Theory]
[InlineData(1)]
public void AddToCartShouldPopulateSessionCartIfMissing(int albumId)
    => MyController<ShoppingCartController>
        .Instance()
        .WithData(new Album { AlbumId = albumId })
        .Calling(c => c.AddToCart(albumId, CancellationToken.None))
        .ShouldHave()
        .Session(session => session // <--
            .ContainingEntryWithKey("Session"));
```

Next, let's assert that the cart item is actually saved into the database. We will need to provide a custom shopping cart ID by using the **"WithSession"** method:

```c#
[Theory]
[InlineData(1, "TestCart")]
public void AddToCartShouldSaveTheAlbumsIntoDatabaseAndSession(
    int albumId, 
    string sessionValue)
    => MyController<ShoppingCartController>
        .Instance()
        .WithSession(session => session // <--
            .WithEntry("Session", sessionValue))
        .WithData(new Album { AlbumId = albumId })
        .Calling(c => c.AddToCart(albumId, CancellationToken.None))
        .ShouldHave()
        .Data(data => data // <--
            .WithSet<CartItem>(cartItems => cartItems
                .Any(a => a.AlbumId == albumId && a.CartId == sessionValue)))
        .AndAlso()
        .ShouldReturn()
        .Redirect(result => result
            .ToAction(nameof(ShoppingCartController.Index)));
```

Of course, extracting the magic constants with a theory and an inline data is a preferred way to follow the best practices... :)

## Cache

For the caching assertions, we will need **"MyTested.AspNetCore.Mvc.Caching"** as a dependency. Add it to the **"MusicStore.Test.csproj"**:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.Caching package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Caching" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Attributes" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Options" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Session" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

Since the package automatically replaces the default memory cache services with scoped mocks, we no longer need this code in the **"TestStartup"** class:

```c#
services.ReplaceLifetime<IMemoryCache>(ServiceLifetime.Scoped);
```

Remove the above line and rerun all tests to see them pass again. 

Remember! All scoped services reset their state after each test. The cache ones are not an exception.

Now, we are going to write three tests against the **"Index"** action in the **"HomeController"**:

```c#
// code skipped for brevity

var cacheKey = "topselling";
List<Album> albums;
if (!cache.TryGetValue(cacheKey, out albums))
{
    albums = await GetTopSellingAlbumsAsync(dbContext, 6);

    if (albums != null && albums.Count > 0)
    {
        if (_appSettings.CacheDbResults)
        {
            cache.Set(cacheKey, albums, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetPriority(CacheItemPriority.High));
        }
    }
}

return View(albums);

// code skipped for brevity
```

Before we begin, create a **"Data"** folder in your test project and add this class in it:

```c#
using MusicStore.Models;
using System.Linq;

public class AlbumData
{
    public static Album[] Many
    {
        get
        {
            var genres = Enumerable.Range(1, 10).Select(n =>
            new Genre()
            {
                GenreId = n,
                Name = "Genre Name " + n,
            }).ToArray();

            var artists = Enumerable.Range(1, 10).Select(n =>
                new Artist()
                {
                    ArtistId = n + 1,
                    Name = "Artist Name " + n,
                }).ToArray();

            var albums = Enumerable.Range(1, 10).Select(n =>
                new Album()
                {
                    Artist = artists[n - 1],
                    ArtistId = n,
                    Genre = genres[n - 1],
                    GenreId = n,
                }).ToArray();

            return albums;
        }
    }
}
```

OK, let's assert the **"HomeController"**! :)

First, we should test that no cache entries are saved if the **"CacheDbResults"** setting is set to **"false"**:

```c#
[Fact]
public void IndexShouldNotUseCacheIfOptionsDisableIt()
    => MyController<HomeController>
        .Instance()
        .WithOptions(options => options
            .For<AppSettings>(settings => settings.CacheDbResults = false))
        .WithData(AlbumData.Many)
        .Calling(c => c.Index(
            From.Services<MusicStoreContext>(),
            From.Services<IMemoryCache>()))
        .ShouldHave()
        .NoMemoryCache(); // <--
```

Unfortunately, the **"NoMemoryCache"** call will not work:

```text
When calling Index action in HomeController expected to have memory cache with no entries, but in fact it had some.
```

With straightforward action debugging we may not see what exactly is going on because the **"CacheDbResults"** is indeed **"false"**. The reason of the error lies in [Entity Framework Core's code](https://github.com/aspnet/EntityFramework/blob/f9adcb64fdf668163377beb14251e67d17f60fa0/src/Microsoft.EntityFrameworkCore/EntityFrameworkServiceCollectionExtensions.cs#L150). It uses the same memory cache service as the web application and guess what! It caches the database query call. But how to debug such issues?

Easy! Add these lines:

```c#
.WithData(AlbumData.Many)
.Calling(c => c.Index(
    From.Services<MusicStoreContext>(),
    From.Services<IMemoryCache>()))
.ShouldPassForThe<IServiceProvider>(services => // <--- add these instead of NoMemoryCache
{
    var memoryCache = services.GetService(typeof(IMemoryCache));;
}); // <--- and put a breakpoint here
```

Running the debugger will allow you to examine the actual values in the cache.

<img src="/images/tutorial/nomemorycachedebug.jpg" alt="Debugging memory cache" />

One of the possible fixes is:

```c#
.Calling(c => c.Index(
    From.Services<MusicStoreContext>(),
    From.Services<IMemoryCache>()))
.ShouldPassForThe<IServiceProvider>(services => Assert.Null(services // <---
    .GetRequiredService<IMemoryCache>().Get("topselling")));
```

You may use custom mocks too, but it is not necessary. In future versions of the library, the above experience will be improved. 

Next, we should assert that with the **"CacheDbResults"** set to **"true"**, we should have saved cache entries from the database query:

```c#
[Theory]
[InlineData(6)]
public void IndexShouldSaveCorrectCacheEntriesIfOptionsEnableIt(int totalAlbums)
    => MyController<HomeController>
        .Instance()
        .WithOptions(options => options
            .For<AppSettings>(settings => settings.CacheDbResults = true))
        .WithData(AlbumData.Many)
        .Calling(c => c.Index(
            From.Services<MusicStoreContext>(),
            From.Services<IMemoryCache>()))
        .ShouldHave()
        .MemoryCache(cache => cache // <--
            .ContainingEntry(entry => entry
                .WithKey("topselling")
                .WithPriority(CacheItemPriority.High)
                .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(10))
                .WithValueOfType<List<Album>>()
                .Passing(albums => albums.Count == totalAlbums)))
        .AndAlso()
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Album>>()
            .Passing(albums => albums.Count == totalAlbums));
```

Finally, we should validate that if the cache contains the albums entry, no database query should be called. We will use an empty database and assert the view model:

```c#
[Theory]
[InlineData(6)]
public void IndexShouldGetAlbumsFromCacheIfEntryExists(int totalAlbums)
    => MyController<HomeController>
        .Instance()
        .WithOptions(options => options
            .For<AppSettings>(settings => settings.CacheDbResults = true))
        .WithMemoryCache(cache => cache
            .WithEntry("topselling", AlbumData.Many.Take(totalAlbums).ToList()))
        .Calling(c => c.Index(
            From.Services<MusicStoreContext>(),
            From.Services<IMemoryCache>()))
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Album>>()
            .Passing(albums => albums.Count == totalAlbums));
```

This way we validate that the entries are retrieved from the cache and not from the actual database (which is empty for this particular test).

## Section summary

Session and cache are fun. By using the **"WithSession"** and **"WithMemoryCache"** methods, you prepare the values to be available during the action call. On the other side, the **"ShouldHave().MemoryCache()"** and the **"ShouldHave().Session()"** extensions allows you to assert their values after the invocation. The same principle applies to the [ViewBag, ViewData & TempData](/tutorial/viewbagviewdatatempdata.html).
