# Session & Cache

In this section we will cover two of the most commonly used modules for data persistence between different requests - session and cache.

## Session

To use the built-in session capabilities of My Tested ASP.NET Core MVC, we need to add **"MyTested.AspNetCore.Mvc.Session"** as a dependency:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Authentication": "1.0.0",
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0",
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.Http": "1.0.0",
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.Options": "1.0.0",
  "MyTested.AspNetCore.Mvc.Session": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MusicStore": "*"
},
```

Adding this package will replace the default session services with scoped mocks, which are empty at the beginning of each test. It's quite easy to test with them. Let's see! :)

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
[Fact]
public void AddToCartShouldPopulateSessionCartIfMissing()
    => MyController<ShoppingCartController>
		.Instance()
		.WithDbContext(db => db
			.WithEntities(entities => entities
				.Add(new Album { AlbumId = 1 })))
		.Calling(c => c.AddToCart(1))
		.ShouldHave()
		.Session(session => session
			.ContainingEntryWithKey("Session"));
```

Next, let's assert that the cart item is actually saved into the database. We will need to provide a custom shopping cart ID by using the **"WithSession"** method:

```c#
[Fact]
public void AddToCartShouldSaveTheAlbumsIntoDatabaseAndSession()
    => MyController<ShoppingCartController>
        .Instance()
        .WithSession(session => session.WithEntry("Session", "TestCart")) // <---
        .WithDbContext(db => db
            .WithEntities(entities => entities
                .Add(new Album { AlbumId = 1 })))
        .Calling(c => c.AddToCart(1))
        .ShouldHave()
        .DbContext(db => db // <---
            .WithSet<CartItem>(cartItems => cartItems
                .Any(a => a.AlbumId == 1 && a.CartId == "TestCart")))
        .AndAlso()
        .ShouldReturn()
        .Redirect()
        .ToAction(nameof(ShoppingCartController.Index));
```

Of course, you can extract the magic strings... :)

## Cache

For the caching assertions, we will need **"MyTested.AspNetCore.Mvc.Caching"** as a dependency. Go and add it to the **"project.json"**:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Authentication": "1.0.0",
  "MyTested.AspNetCore.Mvc.Caching": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0",
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.Http": "1.0.0",
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.Options": "1.0.0",
  "MyTested.AspNetCore.Mvc.Session": "1.0.0",
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MusicStore": "*"
},
```

Since the package automatically replaces the default memory cache services with scoped mocks, we no longer need this code in the **"TestStartup"** class:

```c#
services.ReplaceLifetime<IMemoryCache>(ServiceLifetime.Scoped);
```

Remove the above line and rerun all tests to see them pass again. Remember! All scoped services reset their state for each test. The cache ones are not an exception.

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

Before we begin, add this helper method to the **"HomeControllerTest"** class:

```c#
private static Album[] Albums
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
```

OK, let's assert! :)

First, we should test that no cache entries are saved if the **"CacheDbResults"** setting is set to **"false"**:

```c#
[Fact]
public void IndexShouldNotUseCacheIfOptionsDisableIt()
    => MyController<HomeController>
		.Instance()
		.WithOptions(options => options
			.For<AppSettings>(settings => settings.CacheDbResults = false))
		.WithDbContext(db => db
			.WithEntities(entities => entities.AddRange(Albums)))
		.Calling(c => c.Index(
			From.Services<MusicStoreContext>(),
			From.Services<IMemoryCache>()))
		.ShouldHave()
		.NoMemoryCache(); // <---
```

Unfortunately, the **"NoMemoryCache"** call will not work:

```text
When calling Index action in HomeController expected to have memory cache with no entries, but in fact it had some.
```

With straightforward action debugging we may not see what exactly is going on because the **"CacheDbResults"** is indeed **"false"**. The reason of the error lies in [Entity Framework Core's code](https://github.com/aspnet/EntityFramework/blob/f9adcb64fdf668163377beb14251e67d17f60fa0/src/Microsoft.EntityFrameworkCore/EntityFrameworkServiceCollectionExtensions.cs#L150). It uses the same memory cache service as the web application and guess what! It caches the database query call. But how to debug such issues?

Easy! Add these lines:

```c#
.WithDbContext(db => db
	.WithEntities(entities => entities.AddRange(Albums)))
.Calling(c => c.Index(
	From.Services<MusicStoreContext>(),
	From.Services<IMemoryCache>()))
.ShouldPassForThe<IServiceProvider>(services => // <--- add these instead of NoMemoryCache
{
	var memoryCache = services.GetService<IMemoryCache>();
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

You may use custom mocks too, but it is not necessary.

Next, we should assert that with the **"CacheDbResults"** set to **"true"**, we should have saved cache entries from the database query:

```c#
[Fact]
public void IndexShouldSaveCorrectCacheEntriesIfOptionsEnableIt()
    => MyController<HomeController>
        .Instance()
        .WithOptions(options => options
            .For<AppSettings>(settings => settings.CacheDbResults = true))
        .WithDbContext(db => db
            .WithEntities(entities => entities.AddRange(Albums)))
        .Calling(c => c.Index(
            From.Services<MusicStoreContext>(),
            From.Services<IMemoryCache>()))
        .ShouldHave()
        .MemoryCache(cache => cache // <---
            .ContainingEntry(entry => entry
                .WithKey("topselling")
                .WithPriority(CacheItemPriority.High)
                .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(10))
                .WithValueOfType<List<Album>>()
                .Passing(albums => albums.Count == 6)))
        .AndAlso()
        .ShouldReturn()
        .View()
        .WithModelOfType<List<Album>>()
        .Passing(albums => albums.Count == 6);
```

Finally, we should validate that if the cache contains the albums entry, no database query should be called. We will use an empty database and assert the view model:

```c#
[Fact]
public void IndexShouldGetAlbumsFromCacheIfEntryExists()
    => MyController<HomeController>
        .Instance()
        .WithOptions(options => options
            .For<AppSettings>(settings => settings.CacheDbResults = true))
        .WithMemoryCache(cache => cache
            .WithEntry("topselling", Albums.Take(6).ToList()))
        .Calling(c => c.Index(
            From.Services<MusicStoreContext>(),
            From.Services<IMemoryCache>()))
        .ShouldReturn()
        .View()
        .WithModelOfType<List<Album>>()
        .Passing(albums => albums.Count == 6);
```

This way we validate that the entries are retrieved from the cache and not from the actual database (which is empty for this particular test).

## Section summary

Session and cache are fun. By using the **"WithSession"** and **"WithMemoryCache"** methods, you prepare the values to be available during the action call. On the other side, the **"ShouldHave().MemoryCache()"** and **"ShouldHave().Session()"** extensions allows you to assert their values after the invocation. The same principle applies to the [ViewBag, ViewData & TempData](/tutorial/viewbagviewdatatempdata.html).