# Database

In this section you will get familiar with how helpful the fluent testing library is with an Entity Framework Core database. Despite the data storage abstraction you use (repository pattern, unit of work, etc.), the **"DbContext"** testing has never been easier. And you don't even need a mocking framework! How cool is that? :)

## The scoped in-memory database

Let's try to test an action using the **"DbContext"**. An easy one is **"Index"** in **"StoreController"**:

```c#
public async Task<IActionResult> Index()
{
	var genres = await DbContext.Genres.ToListAsync();

	return View(genres);
}
```

Create a **"StoreControllerTest"** class, add the necessary usings and try to test the action:

```c#
[Fact]
public void IndexShouldReturnViewWithGenres()
    => MyController<StoreController>
        .Instance()
        .Calling(c => c.Index())
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Genre>>());
``` 

A nice little test. With a big "KABOOM"!

```text
When calling Index action in StoreController expected no exception but AggregateException (containing ArgumentException with 'Format of the initialization string does not conform to specification starting at index 0.' message) was thrown without being caught.
```

Not cool for sure! The exception occurs because our **"config.json"** file contains a dummy (and invalid) connection string:

```json
"Data": {
  "DefaultConnection": {
    "ConnectionString": "Test Connection"
  }
}
```

And we should be happy about it! The last thing we want is our tests knowing where the application database is.

But we still need to write a test against the **"DbContext"**! Fear no more - go to the **"MusicStore.Test.csproj"** file and add **"MyTested.AspNetCore.Mvc.EntityFrameworkCore"** as a dependency:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.EntityFrameworkCore package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

Now run the test again and see the magic! :)

Wuuut! I can't believe it! It passes! And we didn't even touch the code! There must be some voodoo involved around here!

As we mentioned earlier - no developer should love magic so here is the trick revealed. The **"EntityFrameworkCore"** package contains a test plugin, which recognizes the **"DbContext"** related services and replaces them with scoped in-memory ones. More information about test plugins can be found [HERE](/guide/plugins.html).

Our test passes but it will be better if we assert the action with actual data. Change the test to:

```c#
MyController<StoreController>
    .Instance()
    .WithData(data => data // <---
        .WithEntities(entities => entities.AddRange(
            new Genre { Name = "FirstGenre" },
            new Genre { Name = "SecondGenre" })))
    .Calling(c => c.Index())
    .ShouldReturn()
    .View(result => result
        .WithModelOfType<List<Genre>>()
        .Passing(model => model.Count == 2));
```

A shorter syntax is also available:

```c#
MyController<StoreController>
    .Instance()
    .WithData( // <---
        new Genre { Name = "FirstGenre" },
        new Genre { Name = "SecondGenre" })
    .Calling(c => c.Index())
    .ShouldReturn()
    .View(result => result
        .WithModelOfType<List<Genre>>()
        .Passing(model => model.Count == 2));
```

The good part of this test is the fact that these data objects live only in-memory and are not stored anywhere.

The best part of this test is the fact that these data objects live in "scoped per test lifetime". We will dive deeper into scoped services during the next tutorial section. For now, write these two tests and run them:

```c#
[Fact]
public void IndexShouldReturnViewWithGenres()
    => MyController<StoreController>
        .Instance()
        .WithData(
            new Genre { Name = "FirstGenre" },
            new Genre { Name = "SecondGenre" })
        .Calling(c => c.Index())
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Genre>>()
            .Passing(model => model.Count == 2));

[Fact]
public void IWillShowScopedDatabaseServices()
    => MyController<StoreController>
        .Instance()
        .WithData(new Genre { Name = "ThirdGenre" })
        .Calling(c => c.Index())
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Genre>>()
            .Passing(model => 
                model.Count == 1 &&
                model.TrueForAll(g => g.Name == "ThirdGenre")));
```

Both tests pass successfully. They are almost the same, but you can notice the difference in the database objects. The first test adds two entities and passes the predicate expecting two objects in the returned list. The second test adds another entity and passes the expectation of having a single genre with a specific name. It is evident the database is fresh, clean, and empty while running each test. This is the power of the scoped test services - they allow each test to be run in an isolated and asynchronous environment.

## Asserting saved database changes

Remove the second test as it is not needed. We will now examine how we can assert saved database objects. For this purpose we are going to use the **"Create"** action (the HTTP POST one) in the **"StoreManagerController"** (located in the **"Admin"** area):

```c#
public async Task<IActionResult> Create(
	Album album,
	[FromServices] IMemoryCache cache,
	CancellationToken requestAborted)
{
	if (ModelState.IsValid)
	{
		DbContext.Albums.Add(album);
		await DbContext.SaveChangesAsync(requestAborted);

		cache.Remove("latestAlbum");
		return RedirectToAction("Index");
	}

	// action code skipped for brevity
}
```

The action expects an **"IMemoryCache"** service, and since we will cover caching later in this tutorial, we will need a cache mock. Add **"Moq"** to the **"MusicStore.Test.csproj"** dependencies:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
	<!-- Moq package -->
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

In **"Controllers"**, create an **"Admin"** folder. In it create a **"StoreManagerControllerTest"** class, add the necessary usings, and write the following test:

```c#
[Fact]
public void CreateShouldSaveAlbumWithValidModelStateAndRedirect()
{
    var album = new Album
    {
        AlbumId = 1,
        Title = "TestAlbum",
        Price = 50
    };

    MyController<StoreManagerController>
        .Instance()
        .Calling(c => c.Create(
            album,
            Mock.Of<IMemoryCache>(),
            With.Default<CancellationToken>()))
        .ShouldHave()
        .ValidModelState()
        .AndAlso()
        .ShouldHave()
        .Data(data => data
            .WithSet<Album>(albums => albums
                .Any(dataAlbum => dataAlbum.AlbumId == album.AlbumId)))
        .AndAlso()
        .ShouldReturn()
        .Redirect(result => result
            .ToAction(nameof(StoreManagerController.Index)));
}
```

The actual database assertion is in the following lines:

```c#
.ShouldHave()
.Data(data => data
    .WithSet<Album>(albums => albums
        .Any(dataAlbum => dataAlbum.AlbumId == album.AlbumId)))
```

My Tested ASP.NET Core MVC validates that the database set of albums should have the saved album with the correct **"AlbumdId"**. As with the previous example, the in-memory database will be empty before the test runs. You may notice the **"With.Default"** call. It's just a more expressive way to write **"new CancellationToken()"**. Providing **"CancellationToken.None"** is also an option.

## Repository pattern

We will take a look at the repository pattern as a small deviation from the Music Store web application. As long as you use the Entity Framework Core's **"DbContext"** class in your web application, the scoped in-memory database will work correctly no matter the data abstraction layer. Imagine we had the following repository registered as a service in our web application:

```c#
public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly MyDbContext db;

    public Repository(MyDbContext db)
    {
        this.db = db;
    }

    public IQueryable<T> All() => this.db.Set<T>();
}
```

And a controller using it:

```c#
public class HomeController : Controller
{
    private IRepository<Album> albums;

    public HomeController(IRepository<Album> albums)
    {
        this.albums = albums;
    }

    public IActionResult Index()
    {
        var latestAlbums = this.albums
            .All()
            .OrderByDescending(a => a.AlbumId)
            .Take(10)
            .ToList();

        return this.Ok(latestAlbums);
    }
}
```

Testing the **"Index"** action does not require anything more than adding lots of albums to the **"DbContext"** and test whether the result list contains exactly 10 elements (you may test the sorting too):

```c#
MyController<HomeController>
    .Instance()
    .WithData(data => data
        .WithSet<Album>(set => AddAlbums(set)))
    .Calling(c => c.Index())
    .ShouldReturn()
    .Ok(result => result
        .WithModelOfType<List<Album>>()
        .Passing(model => model.Count == 10));
```

Piece of cake! :)

## Section summary

This section showed you one of the many useful built-in services suitable for writing fast and asynchronous tests for the ASP.NET Core Framework. A lot of web applications use a database layer, so it is a crucial point to have a friendly and easy way to assert it without having to lose a lot of development time in writing mocks or stubs. Now, head over to the next important part of our journey - the test [Services](/tutorial/services.html)!