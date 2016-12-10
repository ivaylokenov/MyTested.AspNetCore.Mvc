<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyTested.AspNetCore.Mvc/master/tools/logo.png" align="left" alt="MyTested.AspNetCore.Mvc" width="100">&nbsp; MyTested.AspNetCore.Mvc - Fluent testing<br />&nbsp; framework for ASP.NET Core MVC</h1>
====================================

MyTested.AspNetCore.Mvc is a unit testing library providing an easy fluent interface to test the [ASP.NET Core MVC](https://github.com/aspnet/Mvc) framework. It is testing framework agnostic so that you can combine it with a test runner of your choice (e.g. xUnit, NUnit, etc.).

[![Build status](https://ci.appveyor.com/api/projects/status/3xlag3a7f87bg4on?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mytested-aspnetcore-mvc) [![NuGet Badge](https://buildstats.info/nuget/MyTested.AspNetCore.Mvc)](https://www.nuget.org/packages/MyTested.AspNetCore.Mvc/)

## Getting started

It is strongly advised to start with the [tutorial](http://docs.mytestedasp.net/tutorial/intro.html) in order to get familiar with MyTested.AspNetCore.Mvc. Additionally, you may see the [testing guide](http://docs.mytestedasp.net/guide/intro.html) or the [API reference](http://docs.mytestedasp.net/api/index.html) for a full list of available features. MyTested.AspNetCore.Mvc is 100% covered by [more than 1800 unit tests](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/master/test/) and should work correctly. Almost all items in the [issues page](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/issues) are expected future features and enhancements.

## Installation

You can install this library using NuGet into your test project (or reference it directly in your `project.json` file). Currently MyTested.AspNetCore.Mvc is fully compatible with ASP.NET Core MVC 1.1.0 and all older versions available on the official NuGet feed.

    Install-Package MyTested.AspNetCore.Mvc.Universe

This package will include all available assertion methods in your test project, including ones for authentication, database, session, caching and more. If you want only the MVC related features, install `MyTested.AspNetCore.Mvc`. If you want to use the completely **FREE** and **UNLIMITED** version of the library, install only `MyTested.AspNetCore.Mvc.Lite` and no other package. Additionally, if you prefer, you can be more specific by including only some of the packages:

 - `MyTested.AspNetCore.Mvc.Controllers` - Contains setup and assertion methods for controllers
 - `MyTested.AspNetCore.Mvc.Models` - Contains setup and assertion methods for response and view models
 - `MyTested.AspNetCore.Mvc.Routing` - Contains setup and assertion methods for routes
 - `MyTested.AspNetCore.Mvc.Core` - Contains setup and assertion methods for MVC core features
 - `MyTested.AspNetCore.Mvc.TempData` - Contains setup and assertion methods for `ITempDataDictionary`
 - `MyTested.AspNetCore.Mvc.ViewData` - Contains assertion methods for `ViewDataDictionary` and dynamic `ViewBag`
 - `MyTested.AspNetCore.Mvc.ViewActionResults` - Contains setup and assertion methods for view action results
 - `MyTested.AspNetCore.Mvc.ViewComponents` - Contains setup and assertion methods for view components
 - `MyTested.AspNetCore.Mvc.ViewFeatures` - Contains setup and assertion methods for MVC view features
 - `MyTested.AspNetCore.Mvc.Http` - Contains setup and assertion methods for HTTP context, request and response
 - `MyTested.AspNetCore.Mvc.Authentication` - Contains setup methods for `ClaimsPrincipal`
 - `MyTested.AspNetCore.Mvc.ModelState` - Contains setup and assertion methods for `ModelStateDictionary` validations
 - `MyTested.AspNetCore.Mvc.DataAnnotations` - Contains setup and assertion methods for data annotation validations
 - `MyTested.AspNetCore.Mvc.EntityFrameworkCore` - Contains setup and assertion methods for `DbContext`
 - `MyTested.AspNetCore.Mvc.DependencyInjection` - Contains setup methods for dependency injection services
 - `MyTested.AspNetCore.Mvc.Caching` - Contains setup and assertion methods for `IMemoryCache`
 - `MyTested.AspNetCore.Mvc.Session` - Contains setup and assertion methods for `ISession`
 - `MyTested.AspNetCore.Mvc.Options` - Contains setup and assertion methods for `IOptions`
 - `MyTested.AspNetCore.Mvc.Helpers` - Contains additional helper methods for easier assertions
 - `MyTested.AspNetCore.Mvc.Lite` - Completely **FREE** and **UNLIMITED** version of the library. It should not be used in combination with any other package. Includes `Controllers`, `ViewActionResults` and `ViewComponents`.
 
After the downloading is complete, just add `using MyTested.AspNetCore.Mvc;` to your source code and you are ready to test in the most elegant and developer friendly way.
	
    using MyTested.AspNetCore.Mvc;
	
For other interesting packages check out:

 - [MyTested.WebApi](https://github.com/ivaylokenov/MyTested.WebApi) - fluent testing framework for ASP.NET Web API 2
 - [MyTested.HttpServer](https://github.com/ivaylokenov/MyTested.HttpServer) - fluent testing framework for remote HTTP servers
 - [AspNet.Mvc.TypedRouting](https://github.com/ivaylokenov/AspNet.Mvc.TypedRouting) - typed routing and link generation for ASP.NET Core MVC
 - [ASP.NET MVC 5 Lambda Expression Helpers](https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers) - typed expression based link generation for ASP.NET MVC 5
	
## How to use

Make sure to check out the [tutorial](http://docs.mytestedasp.net/tutorial/intro.html) and the [testing guide](http://docs.mytestedasp.net/guide/intro.html) for a preview of the available features.

You can also check out the [provided samples](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/master/samples) for real-life ASP.NET Core MVC application testing.

The easiest way to start with MyTested.AspNetCore.Mvc is to create a `TestStartup` class at the root of the test project in order to register the dependency injection services which will be used by all test cases in the assembly. A quick solution is to inherit from the web project's `Startup` class and replace some of the services with mocked ones by using the provided extension methods.

```c#
namespace MyApp.Tests
{
	using MyTested.AspNetCore.Mvc;
	
    using Microsoft.Extensions.DependencyInjection;

	public class TestStartup : Startup
	{
		public void ConfigureTestServices(IServiceCollection services)
		{
			base.ConfigureServices(services);
			
			services.Replace<IService, MockedService>();
		}
	}
}
```

And then you can create a test case by using the fluent API the library provides. You are given a static `MyMvc` class from which all assertions can be easily configured.

```c#
namespace MyApp.Tests.Controllers
{
	using MyTested.AspNetCore.Mvc;
	
    using MyApp.Controllers;
	using Xunit;

    public class HomeControllerShould
    {
        [Fact]
        public void ReturnViewWhenCallingIndexAction()
        {
            MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Index())
                .ShouldReturn()
				.View();
        }
	}
}
```

Basically, MyTested.AspNetCore.Mvc throws an unhandled exception with a friendly error message if the assertion does not pass and the test fails. The example uses [xUnit](http://xunit.github.io/), but you can use any other framework you like. See the [samples](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/master/samples) for other types of test runners and `Startup` class configurations.

## Examples

Here are some random examples of what the fluent testing API is capable of:

```c#
// tests a route for correct controller, action and resolved route values
MyMvc
	.Routing()
	.ShouldMap(request => request
		.WithLocation("/My/Action/1")
		.WithMethod(HttpMethod.Post)
		.WithAuthenticatedUser()
		.WithAntiForgeryToken()
		.WithJsonBody(new
		{
			Integer = 1,
			String = "Text"
		}))
	.To<MyController>(c => c.Action(1, new MyModel
	{
		Integer = 1,
		String = "Text"
	}))
	.AndAlso()
	.ToValidModelState();

// instantiates controller with the registered services
// and mocks authenticated user
// and tests for valid model state
// and tests for valid view bag entry
// and tests model from view result with specific assertions
MyMvc
    .Controller<MvcController>()
    .WithAuthenticatedUser(user => user.WithUsername("MyUserName"))
    .Calling(c => c.SomeAction(requestModel))
    .ShouldHave()
    .ValidModelState()
	.AndAlso()
	.ShouldHave()
	.ViewBag(viewBag => viewBag
		.ContainingEntry("MyViewBagProperty", "MyViewBagValue"))
    .AndAlso()
    .ShouldReturn()
    .View()
    .WithModelOfType<ResponseModel>()
    .Passing(m =>
    {
        Assert.AreEqual(1, m.Id);
        Assert.AreEqual("Some property value", m.SomeProperty);
    });

// instantiates controller with the registered services
// and sets options for the current test
// and sets session for the current test
// and sets DbContext for the current test
// and tests for added cache entry by the action
// and tests model from view result
MyMvc
	.Controller<MvcController>()
	.WithOptions(options => options
		.For<AppSettings>(settings => settings.Cache = true))
	.WithSession(session => session
		.WithEntry("Session", "SessionValue"))
	.WithDbContext(db => db.WithEntities(entities => entities
		.AddRange(SampleDataProvider.GetModels())))
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.MemoryCache(cache => cache
		.ContainingEntry(entry => entry
			.WithKey("CacheEntry")
			.WithSlidingExpiration(TimeSpan.FromMinutes(10))
			.WithValueOfType<CachedModel>()
			.Passing(a => a.Id == 1)))
	.AndAlso()
	.ShouldReturn()
	.View()
	.WithModelOfType<ResponseModel>();
	
// tests whether the action is declared with filters
MyMvc
	.Controller<MvcController>()
	.Calling(c => c.MyAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForHttpMethod(HttpMethod.Get)
		.AllowingAnonymousRequests()
		.ValidatingAntiForgeryToken()
		.ChangingActionNameTo("AnotherAction"));
	
// tests whether model state error exists by using lambda expression
// and with specific tests for the error messages
// and tests whether the action returns view with the same request model
MyMvc
	.Controller<MvcController>()
	.Calling(c => c.MyAction(requestWithErrors))
	.ShouldHave()
	.ModelState(modelState => modelState.For<RequestModel>()
		.ContainingNoErrorFor(m => m.NonRequiredProperty)
		.AndAlso()
		.ContainingErrorFor(m => m.RequiredProperty)
		.ThatEquals("The RequiredProperty field is required."))
	.AndAlso()
	.ShouldReturn()
	.View(requestWithErrors);

// tests whether the action throws
// with an exception of particular type and with particular message
MyMvc
	.Controller<MvcController>()
	.Calling(c => c.ActionWithException())
	.ShouldThrow()
	.Exception()
	.OfType<NullReferenceException>()
	.AndAlso()
	.WithMessage().ThatEquals("Test exception message");
	
// all applicable methods are available
// on view component testing too
MyMvc
	.ViewComponent<MvcComponent>()
	.WithSession(session => session
		.WithEntry("Session", "SessionValue"))
	.WithDbContext(db => db.WithEntities(entities => entities
		.AddRange(SampleDataProvider.GetModels())))
	.InvokedWith(c => c.InvokeAsync(1))
	.ShouldHave()
	.ViewBag(viewBag => viewBag
		.ContainingEntry("TotalItems", 10)
		.ContainingEntry("EntryName", "ViewBagName"))
	.AndAlso()
	.ShouldReturn()
	.View()
	.WithModelOfType<ResponseModel>();
```

## Versioning

My Tested ASP.NET Core MVC follows the ASP.NET Core MVC versions with which the testing framework is fully compatible. Specifically, the *major* and the *minor* versions will be incremented only when the MVC framework has a new official release. For example, version 1.0.0 of the testing framework will be fully compatible with ASP.NET Core MVC 1.0.0, version 1.1.0 will be fully compatible with ASP.NET Core MVC 1.1.0, version 1.3.15 will be fully compatible with ASP.NET Core MVC 1.3.0, and so on. 

The public interface of My Tested ASP.NET Core MVC will not have any breaking changes when the version increases (unless entirely necessary).

## License

Code by Ivaylo Kenov. Copyright 2015-2016 Ivaylo Kenov ([http://mytestedasp.net](http://mytestedasp.net))

MyTested.AspNetCore.Mvc.Lite (the **FREE** and **UNLIMITED** version of the testing library) is dual-licensed under either the Apache License, Version 2.0, or the Microsoft Public License (Ms-PL).

The source code of MyTested.AspNetCore.Mvc and its extensions (the full version of the testing library) is available under GNU Affero General Public License/FOSS License Exception. 

Without a license code, the full version of the library allows up to 100 assertions (around 25 test cases) per test project.

**Full-featured license codes can be requested for free by individuals, open-source projects, startups and educational institutions**. See [https://mytestedasp.net/Core/Mvc#free-usage-modal](https://mytestedasp.net/Core/Mvc#free-usage) for more information.

Commercial licensing with premium support options is also available at [https://mytestedasp.net/Core/Mvc#pricing](https://mytestedasp.net/Core/Mvc#pricing).

See the [LICENSE](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/blob/master/LICENSE) for detailed information.

## Any questions, comments or additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/issues) or send a [pull request](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/pulls). For general questions and comments, use the [StackOverflow](http://stackoverflow.com/) forum.
