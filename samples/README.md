MyTested.AspNetCore.Mvc Samples
====================================

Here you can find a few working samples, which will get you started with the library.

## Application Samples

 - **Blog** - fully-featured web application sample showing how to use **MyTested.AspNetCore.Mvc** with the best coding practices in mind. Uses [AutoMapper](https://automapper.org), [Moq](https://github.com/moq/moq4), [Shouldly](https://github.com/shouldly/shouldly) and [xUnit](http://xunit.github.io/).
 - **MusicStore** - sample showing how to use **MyTested.AspNetCore.Mvc** with an automatically resolved `TestStartup` class for testing controllers, view components and routes. Uses the [official ASP.NET Core MVC test application](https://github.com/aspnet/AspNetCore/tree/master/src/MusicStore) and [xUnit](http://xunit.github.io/). 

## Functional Test Samples

 - **AutoFac** - minimalistic functional sample showing how to use **MyTested.AspNetCore.Mvc** with a third-party dependency injection container. Uses [AutoFac](https://autofac.org) and [xUnit](http://xunit.github.io/).
 - **ApplicationParts** - minimalistic functional sample testing whether controllers are found correctly when registered from external assemblies. Uses manual `Startup` configuration and [NUnit](https://github.com/nunit/dotnet-test-nunit).
 - **NoStartup** - minimalistic functional sample showing how to use the testing library without any globally configured `Startup` class. Uses [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest).
 - **WebStartup** - minimalistic functional sample showing how to use the testing library with the web application's `Startup` class. Uses [xUnit](http://xunit.github.io/).
