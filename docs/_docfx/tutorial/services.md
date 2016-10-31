# Services

In this section we will examine the test services concept and how My Tested ASP.NET Core MVC reduces the need to mock the world.

## Global test services

All controllers in the Music Store web application have service dependencies defined in their constructors and actions. For example:

```c#
public class HomeController : Controller
{
    private readonly AppSettings _appSettings;

    public HomeController(IOptions<AppSettings> options)
    {
        _appSettings = options.Value;
    }

	// controller code skipped for brevity
}
``` 

But we never specify any dependency in our tests explicitly. We do not provide a mock for the **"IOptions"** interface anywhere in our test project. So how the testing framework decides which services to use?

Remember the **"TestStartup"** class?

```c#
public class TestStartup : Startup
{
    public TestStartup(IHostingEnvironment hostingEnvironment)
        : base(hostingEnvironment)
    {
    }
}
```

By inheriting from the web **Startup** class, we provide our tests all available services from the web application automatically. For example take a look at this configuration call:

```c#
services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
```

It registers the **"IOptions"** interface, which the testing framework uses to instantiate our **"HomeController"** successfully.

## Replacing inherited services

Like in a typical test scenario, some of the inherited services need to be replaced with mocks. As you saw in the previous section, My Tested ASP.NET Core MVC have built-in ones for the most commonly used services like the **"DbContext"**. However, custom services have to be replaced by the developer so let's replace one. Go to the HTTP Post overload of the **"Login"** action in the **"AccountController"**. We want to test a successful sign in. These are the actual lines of code we are interested in:

```c#
public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
{
    // action code skipped for brevity
	
    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
    if (result.Succeeded)
    {
        _logger.LogInformation("Logged in {userName}.", model.Email);
        return RedirectToLocal(returnUrl);
    }
    
	// action code skipped for brevity
}
```

The **"SignInManager"** class does all the work for us, and it is passed as a dependency to the **"AccountController"**:

```c#
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        _logger = logger;
    }
	
	public UserManager<ApplicationUser> UserManager { get; }

	public SignInManager<ApplicationUser> SignInManager { get; }
	
	// controller code skipped for brevity
}
```

We need to replace the **"SignInManager"** service with a mock.

Go to the **TestStartup** class, and add a **"ConfigureTestServices"** method:

```c#
public class TestStartup : Startup
{
    public TestStartup(IHostingEnvironment hostingEnvironment)
        : base(hostingEnvironment)
    {
    }

    public void ConfigureTestServices(IServiceCollection services)
    {
    }
}
```

By adding this method, we have now overridden the base **"ConfigureServices"** in the web **"Startup"** and it will not be invoked. If you try to run a test, you will see an error about missing service registration. To fix it, you need to manually call the base method in order to reuse all the web application services:

```c#
public void ConfigureTestServices(IServiceCollection services)
{
	base.ConfigureServices(services);
}
```

Now all tests should pass again.

Before replacing the **"SignInManager"**, we need a mock. Let's create a minimalistic manual mock for it. Add **"Mocks"** folder in your test project and create the following class in it:

```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using System.Threading.Tasks;

public class SignInManagerMock : SignInManager<ApplicationUser>
{
	public SignInManagerMock(
		UserManager<ApplicationUser> userManager,
		IHttpContextAccessor contextAccessor,
		IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
		IOptions<IdentityOptions> optionsAccessor,
		ILogger<SignInManager<ApplicationUser>> logger)
		: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
	{
	}

	public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
	{
		if (userName == "valid@valid.com" && password == "valid")
		{
			return Task.FromResult(SignInResult.Success);
		}

		return Task.FromResult(SignInResult.Failed);
	}
}
```

We have created a **"SignInManager"** mock which returns successful sign in result, if specific username and password are provided. Otherwise, returns failed result.

Now we need to tell the testing framework to use our mock instead of the actual implementation.

My Tested ASP.NET Core MVC provides a lot of useful extension methods on the service collection. Add this using in the **"TestStartup"** class:

```c#
using MyTested.AspNetCore.Mvc;
```

And then replace the **"SignInManager"** service by writing the following:

```c#
public void ConfigureTestServices(IServiceCollection services)
{
	base.ConfigureServices(services);
	
	services.ReplaceSingleton<SignInManager<ApplicationUser>, SignInManagerMock>();
}
```

From now on, during testing, all injectable constructors will receive the mock instead of the original sign in manager service.

The **"ReplaceSingleton"** method will find a singleton implementation of the service and replace it with the type we want. Other commonly used methods are:

- **"ReplaceTransient"**, **"ReplaceScoped"**, **"ReplaceSingleton"** - these replace the service without changing its lifetime
- **"ReplaceLifetime"** - replaces the lifetime of the service without changing its implementation 
- **"Replace"** - allows replacing both the service implementation and its lifetime
- **"RemoveTransient"**, **"RemoveScoped"**, **"RemoveSingleton"** - these remove the service with the corresponding lifetime
- **"Remove"** - removes the service no matter its lifetime

Use them wisely! :)

Now, let's write the actual test. Create an **"AccountControllerTest"** class and add this test:

```c#
[Fact]
public void LoginShouldReturnRedirectToLocalWithValidLoginViewModel()
{
    var model = new LoginViewModel
    {
        Email = "valid@valid.com",
        Password = "valid"
    };

    var redirectUrl = "/Test/Url";

    MyController<AccountController>
        .Instance()
        .Calling(c => c.Login(model, redirectUrl))
        .ShouldHave()
        .ValidModelState()
        .AndAlso()
        .ShouldReturn()
        .Redirect()
        .ToUrl(redirectUrl);
}
```

And if you like to test for failed login:

```c#
[Fact]
public void LoginShouldReturnViewWithSameModelWithInvalidLoginViewModel()
{
    var model = new LoginViewModel
    {
        Email = "invalid@invalid.com",
        Password = "invalid"
    };

    var redirectUrl = "/Test/Url";

    MyController<AccountController>
        .Instance()
        .Calling(c => c.Login(model, redirectUrl))
        .ShouldHave()
        .ModelState(modelState => modelState
            .ContainingError(string.Empty)
            .ThatEquals("Invalid login attempt."))
        .AndAlso()
        .ShouldReturn()
        .View()
        .WithModel(model);
}
```

You may extract the magic strings if you like (you little perfectionist)... :)

Sometimes you cannot specify the mock directly as a generic parameter so you may need to use an implementation factory.

For example, if we use **"Moq"** to create the **"SignInManager"** mock, we can create a **"MockProvider"** class and write the following in it:

```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;
using Moq;
using System.Threading.Tasks;

public static class MockProvider
{
    public static SignInManager<ApplicationUser> SignInManager(UserManager<ApplicationUser> userManager)
    {
        var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            userManager,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            null);

        signInManagerMock
            .Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
            .Returns((string userName, string password, bool isPersistent, bool lockoutOnFailure) =>
            {
                if (userName == "valid@valid.com" && password == "valid")
                {
                    return Task.FromResult(SignInResult.Success);
                }

                return Task.FromResult(SignInResult.Failed);
            });

        return signInManagerMock.Object;
    }
}
```

And in the **"TestStartup"** class we have to use the implementation factory overload:

```c#
services.ReplaceSingleton<SignInManager<ApplicationUser>>(sp => 
    MockProvider.SignInManager(sp.GetRequiredService<UserManager<ApplicationUser>>()));
```

Keep in mind that replacing specific services may need other configurations. For example, replacing the **"IOptions"** interface without touching the **"config.json"** file have to be done with this line:

```c#
services.Configure<AppSettings>(setting => setting.SiteTitle = "Test Site");
```

## TestStartup without inherited services

Of course, in some scenarios you may be a bit scared to inherit all services from the web application directly. So don't to it and take full control, if you like:

```c#
public class TestStartup
{
	public void ConfigureTestServices(IServiceCollection services)
	{
		services.AddMvc();
		
		// add all your test services and mocks here
	}
	
	public void ConfigureTest(IApplicationBuilder app)
	{
		app.UseMvcWithDefaultRoute();
		
		// add all your test application middleware here
	}
}
```

This approach is safer than using inheritance, but you will need to manually keep in sync the services in the web application and the ones in the test project.

## Scoped services

All scoped services live only for a single test and then their state is reset. In the previous section we saw how the **"DbContext"** extension methods take advantage of this feature by clearing the database for each test.

Now, we are going to see how we can use scoped services for more custom purposes.

Let's examine the **"Details"** action in the **"StoreManagerController"**:

```c#
public async Task<IActionResult> Details(
	[FromServices] IMemoryCache cache,
	int id)
{
	var cacheKey = GetCacheKey(id);

	Album album;
	if (!cache.TryGetValue(cacheKey, out album))
	{
		album = await DbContext.Albums
				.Where(a => a.AlbumId == id)
				.Include(a => a.Artist)
				.Include(a => a.Genre)
				.FirstOrDefaultAsync();

		if (album != null)
		{
			if (_appSettings.CacheDbResults)
			{
				//Remove it from cache if not retrieved in last 10 minutes.
				cache.Set(
					cacheKey,
					album,
					new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));
			}
		}
	}

	if (album == null)
	{
		cache.Remove(cacheKey);
		return NotFound();
	}

	return View(album);
}
```

We want to test that with disabled global caching in the **"AppSettings"** options, we dot not try to cache the database result.

First, we need to prepare the **"IMemoryCache"** mock to throw an exception when setting an entry with specific key. Go to the **"MockProvider"** class (or create it, if you haven't already) and add the following:

```c#
public static IMemoryCache ThrowableMemoryCache
{
    get
    {
        var memoryCacheMock = new Mock<IMemoryCache>();
                
        memoryCacheMock
            .Setup(c => c.CreateEntry(
                It.Is<string>(k => k == $"album_{int.MaxValue}")))
            .Throws(new InvalidOperationException("Caching is not available for this test."));

        return memoryCacheMock.Object;
    }
}
```

Our mock will throw exception, if the requested album ID is equal to the maximum integer value. We will not use this mock globally but rather only for the **"Details"** action test.

Next, we need to set the **"CacheDbResults"** property in the registered **"AppSettings"** class to **"false"**. Since we want to have other tests asserting caching functionality, it will not be a good idea to change the global **"config.json"** value.

Enter scoped service set up! :)

Go to the **"project.json"** file in the test project and add **"MyTested.AspNetCore.Mvc.DependencyInjection"** as a dependency:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MusicStore": "*"
},
```

This package will add service related extension methods to the fluent API. Let's write the test without modifying the **"AppSettings"** configuration:

```c#
[Fact]
public void DetailsShouldNotSaveCacheEntryWithDisabledGlobalCache()
	=> MyController<StoreManagerController>
		.Instance()
		.WithDbContext(db => db
			.WithEntities(entities => entities.Add(new Album 
			{ 
				AlbumId = int.MaxValue 
			})))
		.Calling(c => c.Details(MockProvider.ThrowableMemoryCache, int.MaxValue))
		.ShouldReturn()
		.View()
		.WithModelOfType<Album>()
		.Passing(m => m.AlbumId == int.MaxValue);
```

As you can see, we are adding an album with an ID equal to the maximum integer value. Additionally, in the **"Calling"** method we are providing explicitly the memory cache mock which throws invalid operation exception. Running this test will produce an error as expected:

```text
When calling Details action in StoreManagerController expected no exception but AggregateException (containing InvalidOperationException with 'Caching is not available for this test.' message) was thrown without being caught.
```

Let's disable the caching only for this test. Add the following lines of code right after the **"Instance"** call:

```c#
.WithServices(services => services
	.WithSetupFor<IOptions<AppSettings>>(settings => settings
		.Value.CacheDbResults = false))
```

This call will set up the options service to have the **"CacheDbResults"** property set to **"false"** only for this test. Run it only to see it failing again:

```text
The 'WithSetupFor' method can be used only for services with scoped lifetime.
```

Oh, yeah... We are talking about scoped services here. By default all **"IOptions"** ones are singletons. Let's change that for good in the **"TestStartup"** class:

```c#
services.AddScoped<IOptions<AppSettings>, OptionsManager<AppSettings>>();
```

Run the test again and be happy! :)

I will not tell you there is a better way to assert with options services and that we will cover it later in this tutorial! You should know that already... :)

## Per test services

Sometimes you do not want to have the global services injected into the controller's constructor. You have the option to provide test specific service implementations wherever you need them. Let's assert the **"Browse"** action in the **"StoreController"** but without using any of the globally configured services:

```c#
public async Task<IActionResult> Browse(string genre)
{
	var genreModel = await DbContext.Genres
		.Include(g => g.Albums)
		.Where(g => g.Name == genre)
		.FirstOrDefaultAsync();

	if (genreModel == null)
	{
		return NotFound();
	}

	return View(genreModel);
}
```

First, we obviously need mocks for **"MusicStoreContext"** and **"IOptions"**. Go to the **"MockProvider"** class and add these lines:

```c#
public static MusicStoreContext MusicStoreContext
{
    get
    {
        var efServiceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
                
        var serviceProvider = new ServiceCollection()
            .AddDbContext<MusicStoreContext>(db => db
                .UseInMemoryDatabase()
                .UseInternalServiceProvider(efServiceProvider))
            .BuildServiceProvider();

        var dbContext = serviceProvider.GetRequiredService<MusicStoreContext>();

        dbContext.AddRange(new List<Genre>
        {
            new Genre { GenreId = 1, Name = "Pop" },
            new Genre { GenreId = 2, Name = "Rap" },
            new Genre { GenreId = 3, Name = "Rock" }
        });

        dbContext.SaveChanges();

        return dbContext;
    }
}
```

And then you can write your test:

```c#
[Fact]
public void BrowseShouldReturnNotFoundWithInvalidGenre()
    => MyController<StoreController>
        .Instance(new StoreController(
            MockProvider.MusicStoreContext,
            Mock.Of<IOptions<AppSettings>>()))
        .Calling(c => c.Browse("Invalid"))
        .ShouldReturn()
        .NotFound();
```

This way you are providing the instantiated controller directly to the **"Instance"** method specifying the dependencies manually.

You can also use the **"WithServices"** method in the following way:

```c#
[Fact]
public void BrowseShouldReturnCorrectViewModelWithValidGenre()
    => MyController<StoreController>
		.Instance()
		.WithServices( // <---
			MockProvider.MusicStoreContext,
			Mock.Of<IOptions<AppSettings>>())
		.Calling(c => c.Browse("Rap"))
		.ShouldReturn()
		.View()
		.WithModelOfType<Genre>()
		.Passing(model => model.GenreId == 2);
```

Or like this:

```c#
[Fact]
public void BrowseShouldReturnCorrectViewModelWithValidGenre()
    => MyController<StoreController>
		.Instance()
		.WithServices(services => services // <---
			.With(MockProvider.MusicStoreContext)
			.With(Mock.Of<IOptions<AppSettings>>()))
		.Calling(c => c.Browse("Rap"))
		.ShouldReturn()
		.View()
		.WithModelOfType<Genre>()
		.Passing(model => model.GenreId == 2);
```

## FromServices attribute

Some services are injected through the action parameters with the use of the **"FromServices"** attribute like the method we tested in the previous section:

```c#
public async Task<IActionResult> Create(
	Album album,
	[FromServices] IMemoryCache cache, // <---
	CancellationToken requestAborted)
{
	// action code skipped for brevity
}
```

Go to the **"StoreManagerControllerTest"** class and take a look at the unit test:

```c#
// test code skipped for brevity

.Calling(c => c.Create(
	album,
	Mock.Of<IMemoryCache>(), // <---
	With.Default<CancellationToken>()))
	
// test code skipped for brevity
```

We use a mock of the cache but it's local to that specific unit test. While we write more and more unit tests, we may see that we need the same mock object over and over again in various controllers and actions. For example imagine our cache mock had the following functionality and we wanted it in more than one test:

```c#
var cacheEntryMock = new Mock<ICacheEntry>();
cacheEntryMock.SetupGet(e => e.Key).Returns("MyKey");
cacheEntryMock.SetupGet(e => e.Value).Returns("MyValue");

var cacheMock = new Mock<IMemoryCache>();
cacheMock.Setup(c => c.Get(It.Is<string>(k => k == "MyKey"))).Returns(cacheMock);

var cache = cacheMock.Object;
```

It would be the perfect candidate for a globally registered test service. There is a built-in mock for the **"IMemoryCache"** in My Tested ASP.NET Core MVC already, but we will cover it later in the tutorial.

Go to the **"TestStartup"** class and add a replacement for the **"IMemoryCache"** service:

```c#
services.ReplaceSingleton<IMemoryCache>(Mock.Of<IMemoryCache>());
```

Run all the tests and... what the?! Some tests fail? :(

Turns out Entity Framework Core [uses the memory cache service internally](https://github.com/aspnet/EntityFramework/blob/1fa247b038927a7d7438f666dc11253f64e0432d/src/Microsoft.EntityFrameworkCore/Internal/DbContextServices.cs#L96) so we should be very careful with the global cache mock.

The simplest solution is to still use the original memory cache service but with scoped lifetime so it resets its state after each test:

```c#
services.ReplaceLifetime<IMemoryCache>(ServiceLifetime.Scoped);
```

This call will make all our tests pass again.

OK, but how to pass the globally configured cache mock in the action call instead of the local one?

Easy! Just use the helper **"From"** class:

```c#
.Calling(c => c.Create(
    album,
    From.Services<IMemoryCache>(), // <---
    With.Default<CancellationToken>()))
```

You can even combine it with the previous example by specifying one global and one local mock:

```c#
[Fact]
public void BrowseShouldReturnNotFoundWithInvalidGenre()
    => MyController<StoreController>
        .Instance()
		.WithServices(
			MockProvider.MusicStoreContext,
			From.Services<IOptions<AppSettings>>()) // <---
        .Calling(c => c.Browse("Invalid"))
        .ShouldReturn()
        .NotFound();
```

The **"From.Services"** call is useful where you want to use a globally configured test service but need to provide it explicitly. Pretty cool, right? :)

## Section summary

I hope you are beginning to love My Tested ASP.NET Core MVC. I sure do! :)

We mastered the most important part of the testing framework - the test services. Let's move on and focus on [HTTP & Authentication](/tutorial/httpauthentication.html) now!