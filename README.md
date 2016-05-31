MyTested.Mvc - Fluent testing<br />&nbsp; for ASP.NET Core MVC</h1>
====================================

MyTested.Mvc is a unit testing library providing easy fluent interface to test the [ASP.NET Core MVC](https://github.com/aspnet/Mvc) framework. It is testing framework agnostic, so you can combine it with a test runner of your choice (e.g. xUnit, NUnit, etc.).

## Getting started

It is strongly advised to start with the [tutorial](http://ivaylokenov.github.io/MyTested.Mvc/tutorial/intro.html) in order to get familiar with MyTested.Mvc. Additionally, you may see the [documentation](http://ivaylokenov.github.io/MyTested.Mvc/guide/intro.html) for full list of available features. MyTested.Mvc is 100% covered by [more than 1500 unit tests](https://github.com/ivaylokenov/MyTested.Mvc/tree/master/test/) and should work correctly. Almost all items in the [issues page](https://github.com/ivaylokenov/MyTested.Mvc/issues) are expected future features and enhancements.

## Installation

You can install this library using NuGet into your test project (or reference it directly in your `project.json` file). Currently MyTested.Mvc works with ASP.NET Core MVC RC2.

    Install-Package MyTested.Mvc

After the downloading is complete, just add `using MyTested.Mvc;` to your source code and you are ready to test in the most elegant and developer friendly way.
	
    using MyTested.Mvc;
	
For other interesting packages check out:

 - [MyTested.WebApi](https://github.com/ivaylokenov/MyTested.WebApi) - fluent testing framework for ASP.NET Web API 2
 - [MyTested.HttpServer](https://github.com/ivaylokenov/MyTested.HttpServer) - fluent testing framework for remote HTTP servers
 - [AspNet.Mvc.TypedRouting](https://github.com/ivaylokenov/AspNet.Mvc.TypedRouting) - typed routing and link generation for ASP.NET Core MVC
 - [ASP.NET MVC 5 Lambda Expression Helpers](https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers) - typed expression based link generation for ASP.NET MVC 5
	
## How to use

Make sure to check out the [tutorial](http://ivaylokenov.github.io/MyTested.Mvc/tutorial/intro.html) and the [documentation](http://ivaylokenov.github.io/MyTested.Mvc/guide/intro.html) for a preview of the available features.
You can also check out the [provided samples](https://github.com/ivaylokenov/MyTested.Mvc/tree/master/samples) for real-life ASP.NET Core MVC application testing.

Basically you can create a test case by using the fluent API the library provides. You are given a static `MyMvc` class from which all assertions can be easily configured.

```c#
namespace MyApp.Tests.Controllers
{
	using MyTested.Mvc;
	
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

The example uses [xUnit](http://xunit.github.io/) but you can use whatever testing framework you want.
Basically, MyTested.Mvc throws an unhandled exception with a friendly error message if the assertion does not pass and the test fails.

Here are some random examples of what the fluent testing API is capable of:

```c#
// tests a route for correct controller, action and resolved route values


// injects dependencies into controller
// and mocks authenticated user
// and tests for valid model state
// and tests response model from Ok result with specific assertions

// tests whether model state error exists by using lambda expression
// and specific tests for the error messages
	
// tests whether the action throws internal server error
// with exception of certain type and with certain message
	
// run full pipeline integration test

// ADD MORE SAMPLES
```

## License

Code by Ivaylo Kenov. Copyright 2015 Ivaylo Kenov ([http://mytestedasp.net](http://mytestedasp.net))

MyTested.Mvc source code is available under GNU Affero General Public License/FOSS License Exception. Additionally, full-featured licenses can be requested for free by individuals, startups and educational institutions. Commercial licensing with private support is also available.

See [https://mytestedasp.net/products/mvc#pricing](https://mytestedasp.net/products/mvc#pricing) and the [LICENSE](https://github.com/ivaylokenov/MyTested.Mvc/blob/master/LICENSE) for detailed information.
 
## Any questions, comments or additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ivaylokenov/MyTested.Mvc/issues) or send a [pull request](https://github.com/ivaylokenov/MyTested.Mvc/pulls). For general questions and comments, use the [StackOverflow](http://stackoverflow.com/) forum.