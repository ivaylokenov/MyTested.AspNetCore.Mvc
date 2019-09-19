<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyTested.AspNetCore.Mvc/master/tools/logo.png" align="left" alt="MyTested.AspNetCore.Mvc" width="100">&nbsp; MyTested.AspNetCore.Mvc - Fluent Testing<br />&nbsp; Library for ASP.NET Core MVC</h1> 

## Gold Sponsors

<table>
  <tbody>
    <tr>
      <td align="center" valign="middle">
        <a href="https://softuni.org/" target="_blank">
          <img width="148px" src="https://softuni.org/platform/assets/icons/logo.svg">
        </a>
      </td>
	    <td align="center" valign="middle">
        <a href="http://bit.ly/30xsnsC" target="_blank">
          <img width="148px" src="https://user-images.githubusercontent.com/3391906/65251792-dd848800-daef-11e9-8857-637a48048cda.png">
        </a>
      </td>
    </tr>
  </tbody>
</table>

## Project Description

**MyTested.AspNetCore.Mvc** is a strongly-typed unit testing library providing an easy fluent interface to test the [ASP.NET Core MVC](https://github.com/aspnet/AspNetCore) framework. It is testing framework agnostic so that you can combine it with a test runner of your choice (e.g. [xUnit](https://github.com/xunit/xunit), [NUnit](https://github.com/nunit/nunit), etc.). 

*Windows:* [![Build status](https://ci.appveyor.com/api/projects/status/3xlag3a7f87bg4on?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mytested-aspnetcore-mvc)

*Ubuntu & Mac OS:* [![Build Status](https://travis-ci.org/ivaylokenov/MyTested.AspNetCore.Mvc.svg?branch=development)](https://travis-ci.org/ivaylokenov/MyTested.AspNetCore.Mvc) 

*Downloads:* [![NuGet Badge](https://buildstats.info/nuget/MyTested.AspNetCore.Mvc)](https://www.nuget.org/packages/MyTested.AspNetCore.Mvc/)

<img src="https://raw.githubusercontent.com/ivaylokenov/MyTested.AspNetCore.Mvc/version-2.2/tools/test-sample.gif" />

**MyTested.AspNetCore.Mvc** has [more than 500 assertion methods](https://MyTestedASP.NET/Core/Mvc/Features) and is 100% covered by [more than 2000 unit tests](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/version-2.2/test). It should work correctly. Almost all items in the [issues page](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/issues) are expected future features and enhancements.

**MyTested.AspNetCore.Mvc** helps you speed up the testing process in your web development team! If you find that statement unbelievable, these are the words which some of the many happy **MyTested.AspNetCore.Mvc** users once said: 
> "I’ve been using your packages for almost 3 years now and it has saved me countless hours in creating unit tests and wanted to thank you for making this. I cannot imagine how much code I would have had to write to create the 450+ and counting unit tests I have for my controllers."

> "I absolutely love this library and it greatly improved the unit/integration test experience in my team."

> ["Amazing library, makes you want to do test-driven development, thanks!"](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/issues/265#issue-194578165)

> "Wanted to thank you for your effort and time required to create this. This is a great tool! Keep up the good work."

Take a look around and...

⭐️ ...if you like the library, **star** the repository and **show** it to your friends!

😏 ...if you find it useful, make sure you **subscribe** for future releases by clicking the **"Watch"** button and choosing **"Releases only"**!

👀 ...if you want to learn cool C# coding techniques, **subscribe** to my [YouTube channel](https://www.youtube.com/channel/UCP5Ons7fK3yKhX6lhc9XcfQ), where I regularly post online video lessons!

✔ ...if you want to **support** the project, **[become a sponsor/backer](#sponsors--backers)** or go to [https://MyTestedASP.NET](https://MyTestedASP.NET), and consider **purchasing a premium [license](#license)**!

#### Featured in

- [The official ASP.NET Core MVC repository](https://github.com/aspnet/AspNetCore/tree/master/src/Mvc#aspnet-core-mvc)
- [NuGet Package of the week in "The week in .NET – 6/28/2016"](https://devblogs.microsoft.com/dotnet/the-week-in-net-6282016/)
- [Awesome .NET Core](https://github.com/thangchung/awesome-dotnet-core#testing)

## Sponsors &amp; Backers

**MyTested.AspNetCore.Mvc** is a community-driven open source library. It's an independent project with its ongoing development made possible thanks to the support by these awesome [backers](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/blob/development/BACKERS.md). If you'd like to join them, please consider:

- [Become a backer or sponsor on Patreon](https://www.patreon.com/ivaylokenov)
- [Become a backer or sponsor on OpenCollective](https://opencollective.com/mytestedaspnet)
- [One-time donation via PayPal](http://paypal.me/ivaylokenov)
- [One-time donation via Buy Me A Coffee](http://buymeacoff.ee/ivaylokenov)
- One-time donation via cryptocurrencies:
  - BTC (Bitcoin) - 3P49XMiGXxqR2Dq1HdqHpkCa6UD848rpBU 
  - BCH (Bitcoin Cash) - qqgyjlvmuydf6gtfhfdypyw2u8utmc3uqg4nwma3y4
  - ETC (Ethereum) - 0x2bc55e4b1B9b296B751738631CD24b2f701E588F
  - LTC (Litecoin) - MQ1GJum1QuqAuUsc6LarE3Z6TQQJ3rJwsA

#### What's the difference between Patreon and OpenCollective?

Funds donated via both platforms are used for development and marketing purposes. Funds donated via [OpenCollective](https://opencollective.com/mytestedaspnet) are managed with transparent expenses. Your name/logo will receive proper recognition and exposure by donating on either platform.

Additionally, funds donated via [Patreon](https://www.patreon.com/ivaylokenov) (see the stretch goals) give me the freedom to add more features to the free `Lite` edition of the library.

## Quick Start

To add **MyTested.AspNetCore.Mvc** to your solution, you must follow these simple steps:

1. Create a test project.
2. Reference your web application.
3. Install **`MyTested.AspNetCore.Mvc.Universe`** (or just the [testing packages](#package-installation) you need) from [NuGet](https://www.nuget.org/packages/MyTested.AspNetCore.Mvc.Universe/).
4. Open the test project's `.csproj` file.
5. Change the project SDK to `Microsoft.NET.Sdk.Web`:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
```

6. Add a package reference to the web framework - `Microsoft.AspNetCore.App`:

```xml
<PackageReference Include="Microsoft.AspNetCore.App" />
```

7. Your test project's `.csproj` file should be similar to this one:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web"> <!-- Changed project SDK -->

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" /> <!-- Reference to the web framework -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Universe" Version="2.2.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.2.0" />
    <PackageReference Include="xunit" Version="2.4.1" /> <!-- Can be any testing framework --> 
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.1" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\MyApp\MyApp.csproj" /> <!-- Reference to your web project --> 
  </ItemGroup>

</Project>
```

8. Create a `TestStartup` class at the root of the test project to register the dependency injection services, which will be used by all test cases in the assembly. A quick solution is to inherit from the web project's `Startup` class. By default **MyTested.AspNetCore.Mvc** replaces all ASP.NET Core services with ready to be used mocks. You only need to replace your own custom services with mocked ones by using the provided extension methods. 

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
			
            // Replace only your own custom services. The ASP.NET Core ones 
            // are already replaced by MyTested.AspNetCore.Mvc. 
            services.Replace<IService, MockedService>();
        }
    }
}
```

9. Create a test case by using the fluent API the library provides. You are given a static `MyMvc` class from which all assertions can be easily configured:

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
            => MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
    }
}
```

Basically, **MyTested.AspNetCore.Mvc** throws an unhandled exception with a friendly error message if the assertion does not pass and the test fails. The example uses [xUnit](http://xunit.github.io/), but you can use any other framework you like. See the [samples](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/version-2.2/samples) for other types of test runners and `Startup` class configurations.

## Detailed Documentation

It is **strongly advised** to read the [tutorial](http://docs.mytestedasp.net/tutorial/intro.html) or watch [this online video lesson](https://www.youtube.com/watch?v=Tf2P-410Za4) to get familiar with **MyTested.AspNetCore.Mvc** in more details. Additionally, you may see the [testing guide](http://docs.mytestedasp.net/guide/intro.html) or the [API reference](http://docs.mytestedasp.net/api/index.html) for a full list of available features.

You can also check out the [provided samples](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/version-2.2/samples) for real-life ASP.NET Core MVC application testing.

## Package Installation

You can install this library using [NuGet](https://www.nuget.org/packages/MyTested.AspNetCore.Mvc.Universe) into your test project (or reference it directly in your `.csproj` file). Currently **MyTested.AspNetCore.Mvc** is fully compatible with ASP.NET Core MVC 2.2.0 and all older versions available on the official NuGet feed.

```powershell
Install-Package MyTested.AspNetCore.Mvc.Universe
```

This package will include all available assertion methods in your test project, including ones for authentication, database, session, caching and more. If you want only the MVC related features, install `MyTested.AspNetCore.Mvc`. If you want to use the completely **FREE** and **UNLIMITED** version of the library, install only `MyTested.AspNetCore.Mvc.Lite` and no other package. Additionally, if you prefer, you can be more specific by including only some of the packages:

 - `MyTested.AspNetCore.Mvc.Configuration` - Contains setup and assertion methods for configurations
 - `MyTested.AspNetCore.Mvc.Controllers` - Contains setup and assertion methods for controllers
 - `MyTested.AspNetCore.Mvc.Controllers.Attributes` - Contains setup and assertion methods for controller attributes
 - `MyTested.AspNetCore.Mvc.Controllers.ActionResults` - Contains setup and assertion methods for controller API action results
 - `MyTested.AspNetCore.Mvc.Controllers.Views` - Contains setup and assertion methods for controller view features
 - `MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults` - Contains setup and assertion methods for controller view action results
 - `MyTested.AspNetCore.Mvc.Models` - Contains setup and assertion methods for response and view models
 - `MyTested.AspNetCore.Mvc.Routing` - Contains setup and assertion methods for routes
 - `MyTested.AspNetCore.Mvc.Core` - Contains setup and assertion methods for MVC core features
 - `MyTested.AspNetCore.Mvc.TempData` - Contains setup and assertion methods for `ITempDataDictionary`
 - `MyTested.AspNetCore.Mvc.ViewData` - Contains assertion methods for `ViewDataDictionary` and dynamic `ViewBag`
 - `MyTested.AspNetCore.Mvc.ViewComponents` - Contains setup and assertion methods for view components
 - `MyTested.AspNetCore.Mvc.ViewComponents.Attributes` - Contains setup and assertion methods for view component attributes
 - `MyTested.AspNetCore.Mvc.ViewComponents.Results` - Contains setup and assertion methods for view component results
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
 - `MyTested.AspNetCore.Mvc.Lite` - Completely **FREE** and **UNLIMITED** version of the library. It should not be used in combination with any other package. Includes `Controllers`, `Controllers.Views` and `ViewComponents`.
 
After the downloading is complete, just add `using MyTested.AspNetCore.Mvc;` to your source code and you are ready to test in the most elegant and developer friendly way.

```c#	
using MyTested.AspNetCore.Mvc;
```

## Test Examples

Here are some examples of how **powerful** the fluent testing API actually is! 

**MyTested.AspNetCore.Mvc** is so **awesome** that each test can be written in **one single line** like in this [application sample](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/version-2.2/samples/Blog)!

### Controller Integration Tests

A controller integration test uses the globally registered services from the `TestStartup` class:

```c#
// Instantiates controller with the registered global services,
// and mocks authenticated user,
// and tests for valid model state,
// and tests for added by the action view bag entry,
// and tests for view result and model with specific assertions.
MyController<MyMvcController>
    .Instance()
    .WithUser(user => user
        .WithUsername("MyUserName"))
    .Calling(c => c.MyAction(myRequestModel))
    .ShouldHave()
    .ValidModelState()
    .AndAlso()
    .ShouldHave()
    .ViewBag(viewBag => viewBag
        .ContainingEntry("MyViewBagProperty", "MyViewBagValue"))
    .AndAlso()
    .ShouldReturn()
    .View(result => result
        .WithModelOfType<MyResponseModel>()
        .Passing(model =>
        {
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("My property value", model.MyProperty);
        }));

// Instantiates controller with the registered global services,
// and sets options for the current test,
// and sets session for the current test,
// and sets DbContext data for the current test,
// and tests for added by the action cache entry,
// and tests for view result with specific model type.
MyController<MyMvcController>
    .Instance()
    .WithOptions(options => options
        .For<MyAppSettings>(settings => settings.Cache = true))
    .WithSession(session => session
        .WithEntry("MySession", "MySessionValue"))
    .WithData(data => data
        .WithEntities(entities => entities
            .AddRange(MyDataProvider.GetMyModels())))
    .Calling(c => c.MyAction())
    .ShouldHave()
    .MemoryCache(cache => cache
        .ContainingEntry(entry => entry
            .WithKey("MyCacheEntry")
            .WithSlidingExpiration(TimeSpan.FromMinutes(10))
            .WithValueOfType<MyCachedModel>()
            .Passing(cacheModel => cacheModel.Id == 1)))
    .AndAlso()
    .ShouldReturn()
    .View(result => result
        .WithModelOfType<MyResponseModel>());

// Instantiates controller with the registered global services,
// and tests for valid model state,
// and tests for saved data in the DbContext after the action call,
// and tests for added by the action temp data entry with а specific key,
// and tests for redirect result to a specific action. 
MyController<MyMvcController>
    .Calling(c => c.MyAction(new MyFormModel
    {
        Title = title,
        Content = content
    }))
    .ShouldHave()
    .ValidModelState()
    .AndAlso()
    .ShouldHave()
    .Data(data => data
        .WithSet<MyModel>(set => set 
            .Should() // Uses FluentAssertions.
            .NotBeEmpty()
            .And
            .ContainSingle(model => model.Title == title)))
    .AndAlso()
    .ShouldHave()
    .TempData(tempData => tempData
        .ContainingEntryWithKey(ControllerConstants.SuccessMessage))
    .AndAlso()
    .ShouldReturn()
    .Redirect(redirect => redirect
        .To<AnotherController>(c => c.AnotherAction()));
```

The last test uses [Fluent Assertions](https://github.com/fluentassertions/fluentassertions) to further enhance the testing API. Another good alternative is [Shouldly](https://github.com/shouldly/shouldly).

### Controller Unit Tests

A controller unit test uses service mocks explicitly provided in each separate assertion: 

```c#
// Instantiates controller with the provided service mocks,
// and tests for view result.
MyController<MyMvcController>
    .Instance()
    .WithDependencies(
        serviceMock,
        anotherServiceMock,
        From.Services<IYetAnotherService>()) // Provides a global service.
    .Calling(c => c.MyAction())
    .ShouldReturn()
    .View();
	
// Instantiates controller with the provided service mocks,
// and tests for view result.
MyController<MyMvcController>
    .Instance()
    .WithDependencies(dependencies => dependencies
        .With<IService>(serviceMock)
        .WithNo<IAnotherService>()) // Provides null for IAnotherService.
    .Calling(c => c.MyAction(From.Services<IYetAnotherService>())) // Provides a global service.
    .ShouldReturn()
    .View();
```

### Route Tests

A route test validates the web application's routing configuration:

```c#
// Tests a route for correct controller, action, and resolved route values.
MyRouting
    .Configuration()
    .ShouldMap("/My/Action/1")
    .To<MyController>(c => c.Action(1));

// Tests a route for correct controller, action, and resolved route values
// with authenticated post request and submitted form.
MyRouting
    .Configuration()
    .ShouldMap(request => request
        .WithMethod(HttpMethod.Post)
        .WithLocation("/My/Action")
        .WithFormFields(new
        {
            Title = title,
            Content = content
        })
        .WithUser()
        .WithAntiForgeryToken())
    .To<MyController>(c => c.Action(new MyFormModel
    {
        Title = title,
        Content = content
    }))
    .AndAlso()
    .ToValidModelState();

// Tests a route for correct controller, action, and resolved route values
// with authenticated post request and JSON body.
MyRouting
    .Configuration()
    .ShouldMap(request => request
        .WithLocation("/My/Action/1")
        .WithMethod(HttpMethod.Post)
        .WithUser()
        .WithAntiForgeryToken()
        .WithJsonBody(new
        {
            Integer = 1,
            String = "Text"
        }))
    .To<MyController>(c => c.Action(1, new MyJsonModel
    {
        Integer = 1,
        String = "Text"
    }))
    .AndAlso()
    .ToValidModelState();
```

### Attribute Declaration Tests

An attribute declaration test validates controller and action attribute declarations:

```c#
// Tests for specific controller attributes - Area and Authorize.
MyController<MyMvcController>
    .ShouldHave()
    .Attributes(attributes => attributes
        .SpecifyingArea(ControllerConstants.AdministratorArea)
        .RestrictingForAuthorizedRequests(ControllerConstants.AdministratorRole));

// Tests for specific action attributes - HttpGet, AllowAnonymous, ValidateAntiForgeryToken, and ActionName.
MyController<MyMvcController>
    .Calling(c => c.MyAction(With.Empty<int>())) // Provides no value for the action parameter.
    .ShouldHave()
    .ActionAttributes(attributes => attributes
        .RestrictingForHttpMethod(HttpMethod.Get)
        .AllowingAnonymousRequests()
        .ValidatingAntiForgeryToken()
        .ChangingActionNameTo("AnotherAction"));
```

### View Component Tests

All applicable methods are available on the view component testing API too:

```c#
// View component integration test.
MyViewComponent<MyMvcController>
    .Instance()
    .WithSession(session => session
        .WithEntry("MySession", "MySessionValue"))
    .WithData(data => data
        .WithEntities(entities => entities
            .AddRange(MyDataProvider.GetMyModels())))
    .InvokedWith(c => c.InvokeAsync(1))
    .ShouldHave()
    .ViewBag(viewBag => viewBag
        .ContainingEntry("MyItems", 10)
        .ContainingEntry("MyViewBagName", "MyViewBagValue"))
    .AndAlso()
    .ShouldReturn()
    .View()
    .WithModelOfType<MyResponseModel>();
	
// View component unit test.
MyViewComponent<MyMvcController>
    .Instance()
    .WithDependencies(
        serviceMock,
        anotherServiceMock,
        From.Services<IYetAnotherService>()) // Provides a global service.
    .InvokedWith(c => c.InvokeAsync(1))
    .ShouldReturn()
    .View();
```

### Arrange, Act, Assert (AAA) Tests

**MyTested.AspNetCore.Mvc** is fully compatible with the AAA testing methodology:

```c#
// Without breaking the fluent API.

MyMvc

    // Arrange
    .Controller<MyMvcController>()
    .WithHttpRequest(request => request
        .WithFormField("MyFormField", "MyFormFieldValue"))
    .WithSession(session => session
        .WithEntry("MySession", sessionId))
    .WithUser()
    .WithRouteData() // Populates the controller route data.
    .WithData(data => data
        .WithEntities(entities => 
            AddData(sessionId, entities)))
			
    // Act
    .Calling(c => c.MyAction(
        From.Services<MyDataContext>(), // Action injected services can be populated with this call.
        new MyModel { Id = id },
        CancellationToken.None))
			
    // Assert
    .ShouldReturn()
    .Redirect(redirect => redirect
        .To<AnotherController>(c => c.AnotherAction(
            With.No<MyDataContext>(),
            id)));
			
// With variables.

// Arrange
var controller = MyController<MyMvcController>
    .Instance()
    .WithUser(username, new[] { role })
    .WithData(MyTestData.GetData());
	
// Act
var call = controller.Calling(c => c.MyAction(id));
    
// Assert
call
    .ShouldReturn()
    .View(view => view
        .WithModelOfType<MyModel>()
        .Passing(model => model.Id == id));
```

### Various Test Examples

Just random **MyTested.AspNetCore.Mvc** assertions:

```c#
// Tests whether model state error exists by using a lambda expression
// with specific tests for the error messages,
// and tests whether the action returns view with the same request model.
MyMvc
    .Controller<MyMvcController>()
    .Calling(c => c.MyAction(myRequestWithErrors))
    .ShouldHave()
    .ModelState(modelState => modelState
        .For<MyRequestModel>()
        .ContainingNoErrorFor(m => m.NonRequiredProperty)
        .AndAlso()
        .ContainingErrorFor(m => m.RequiredProperty)
        .ThatEquals("The RequiredProperty field is required."))
    .AndAlso()
    .ShouldReturn()
    .View(myRequestWithErrors);

// Tests whether the action throws an exception 
// of a particular type and with a particular error message.
MyMvc
    .Controller<MyMvcController>()
    .Calling(c => c.MyActionWithException())
    .ShouldThrow()
    .Exception()
    .OfType<MyException>()
    .AndAlso()
    .WithMessage()
    .ThatEquals("My exception message");

// Tests whether the action return OK result
// and writes to the response specific content type,
// custom header, and custom cookie.
MyMvc
    .Controller<MyMvcController>()
    .Calling(c => c.MyAction())
    .ShouldHave()
    .HttpResponse(response => response
        .WithContentType(ContentType.ApplicationJson)
        .ContainingHeader("MyHeader", "MyHeaderValue")
        .ContainingCookie(cookie => cookie
            .WithName("MyCookie")
            .WithValue("MyCookieValue")
            .WithSecurity(true)
            .WithHttpOnly(true)
            .WithDomain("mydomain.com")
            .WithExpiration(myDateTimeOffset)
            .WithPath("/")))
	.AndAlso()
    .ShouldReturn()
    .Ok();
```

## Versioning

**MyTested.AspNetCore.Mvc** follows the ASP.NET Core MVC versions with which the testing framework is fully compatible. Specifically, the *major* and the *minor* versions will be incremented only when the MVC framework has a new official release. For example, version 2.2.\* of the testing framework is fully compatible with ASP.NET Core MVC 2.2.\*, version 1.1.\* is fully compatible with ASP.NET Core MVC 1.1.\*, version 1.0.15 is fully compatible with ASP.NET Core MVC 1.0.\*, and so on. 

The public interface of **MyTested.AspNetCore.Mvc** will not have any breaking changes when the version increases (unless entirely necessary).

## License

Code by Ivaylo Kenov. Copyright 2015-2019 Ivaylo Kenov ([https://MyTestedASP.NET](https://MyTestedASP.NET))

**MyTested.AspNetCore.Mvc.Lite** (the **FREE** and **UNLIMITED** version of the testing library) is dual-licensed under either the Apache License, Version 2.0, or the Microsoft Public License (Ms-PL).

The source code of **MyTested.AspNetCore.Mvc** and its extensions (the full version of the testing library) is available under GNU Affero General Public License/FOSS License Exception. 

Without a license code, the full version of the library allows up to 100 assertions (around 25 test cases) per test project. **MyTested.AspNetCore.Mvc versions before 3.0.0 do not have such restrictions and work without any limitations.**

**Full-featured license codes can be requested for free by small businesses (up to 5 developers), individuals, open-source projects, startups, and educational institutions**. See [https://MyTestedASP.NET/Core/Mvc#free-usage](https://MyTestedASP.NET/Core/Mvc#free-usage) for more information.

Commercial licensing with premium support options is also available at [https://MyTestedASP.NET/Core/Mvc#pricing](https://MyTestedASP.NET/Core/Mvc#pricing).

See the [LICENSE](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/blob/master/LICENSE) for detailed information.

## Any Questions, Comments or Additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/issues) or send a [pull request](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/pulls). For general questions and comments, use the [StackOverflow](http://stackoverflow.com/) forum.
