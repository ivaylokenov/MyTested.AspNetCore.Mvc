# Organizing Tests

This section will cover the various ways we can organize our tests in a single class.

## The fluent style

This is the test style we used so far in the tutorial. For example, let's take a look at our **"StoreControllerTest"** class:

```c#
namespace MusicStore.Test.Controllers
{
    using Microsoft.Extensions.Options;
    using Moq;
    using MusicStore.Controllers;
    using MusicStore.Models;
    using MusicStore.Test.Mocks;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;

    public class StoreControllerTest
    {
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
        public void BrowseShouldReturnNotFoundWithInvalidGenre()
            => MyController<StoreController>
                .Instance(new StoreController(
                    MockProvider.MusicStoreContext,
                    Mock.Of<IOptions<AppSettings>>()))
                .Calling(c => c.Browse("Invalid"))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void BrowseShouldReturnCorrectViewModelWithValidGenre()
            => MyController<StoreController>
                .Instance()
                .WithDependencies(
                    MockProvider.MusicStoreContext,
                    From.Services<IOptions<AppSettings>>())
                .Calling(c => c.Browse("Rap"))
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<Genre>()
                    .Passing(model => model.GenreId == 2));
    }
}
```

## The classic AAA style

You can split the fluent API in the classic Arrange-Act-Assert style:

```c#
[Fact]
public void IndexShouldReturnViewWithGenres()
{
    // Arrange
    var controller = MyController<StoreController>
        .Instance()
        .WithData(
            new Genre { Name = "FirstGenre" },
            new Genre { Name = "SecondGenre" });

    // Act
    var execution = controller.Calling(c => c.Index());

    // Assert
    execution
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Genre>>()
            .Passing(model => model.Count == 2));
}
```

## The fluent AAA style

Or just mark the fluent API with comments:

```c#
[Fact]
public void IndexShouldReturnViewWithGenres()
    => MyController<StoreController>
        // Arrange
        .Instance()
        .WithData(
            new Genre { Name = "FirstGenre" },
            new Genre { Name = "SecondGenre" })
        // Act
        .Calling(c => c.Index())
        // Assert
        .ShouldReturn()
        .View(result => result
            .WithModelOfType<List<Genre>>()
            .Passing(model => model.Count == 2));
```

## The inheriting style

You may inherit the **"MyController"** class to skip writing it in every single test:

```c#
namespace MusicStore.Test.Controllers
{
    using Microsoft.Extensions.Options;
    using Mocks;
    using Models;
    using Moq;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;

    public class StoreControllerTest : MyController<StoreController> // <---
    {
        [Fact]
        public void IndexShouldReturnViewWithGenres()
            => this
                .WithData(
                    new Genre { Name = "FirstGenre" },
                    new Genre { Name = "SecondGenre" })
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<List<Genre>>()
                    .Passing(model => model.Count == 2));

        [Fact]
        public void BrowseShouldReturnNotFoundWithInvalidGenre()
            => Instance(new StoreController(
                    MockProvider.MusicStoreContext,
                    Mock.Of<IOptions<AppSettings>>()))
                .Calling(c => c.Browse("Invalid"))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void BrowseShouldReturnCorrectViewModelWithValidGenre()
            => this
                .WithDependencies(
                    MockProvider.MusicStoreContext,
                    Mock.Of<IOptions<AppSettings>>())
                .Calling(c => c.Browse("Rap"))
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<Genre>()
                    .Passing(model => model.GenreId == 2));
    }
}
```

**NOTE:** To run the above tests asynchronously, the test runner should instantiate the **"StoreControllerTest"** class for every single test. This is the default behavior of **"xUnit"** so you shouldn't experience any issues if you do not alter its collection parallelism. You can also avoid a race condition if you replace the **"this"** keyword with **"Instance"**:

```c#
namespace MusicStore.Test.Controllers
{
    using Microsoft.Extensions.Options;
    using Mocks;
    using Models;
    using Moq;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;

    public class StoreControllerTest : MyController<StoreController> // <---
    {
        [Fact]
        public void IndexShouldReturnViewWithGenres()
            => Instance() // <---
                .WithData(
                    new Genre { Name = "FirstGenre" },
                    new Genre { Name = "SecondGenre" })
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<List<Genre>>()
                    .Passing(model => model.Count == 2));

        [Fact]
        public void BrowseShouldReturnNotFoundWithInvalidGenre()
            => Instance(new StoreController(
                    MockProvider.MusicStoreContext,
                    Mock.Of<IOptions<AppSettings>>()))
                .Calling(c => c.Browse("Invalid"))
                .ShouldReturn()
                .NotFound();

        [Fact]
        public void BrowseShouldReturnCorrectViewModelWithValidGenre()
            => Instance() // <---
                .WithDependencies(
                    MockProvider.MusicStoreContext,
                    Mock.Of<IOptions<AppSettings>>())
                .Calling(c => c.Browse("Rap"))
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<Genre>()
                    .Passing(model => model.GenreId == 2));
    }
}
```

## Section summary

Of course, you can always combine two or more of the above styles as long as your code is consistent. Now, let's take a look at the framework's [Test Configuration](/tutorial/testconfig.html)!
