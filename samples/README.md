MyTested.AspNetCore.Mvc Samples
====================================

Here you can find a few working samples, which will get you started with the library.

## Application Samples

 - MusicStore - sample showing how to use MyTested.AspNetCore.Mvc with an automatically resolved `TestStartup` class for unit testing controllers, view components and routes. It uses the [official ASP.NET Core MVC sample](https://github.com/aspnet/MusicStore) and [xUnit](http://xunit.github.io/). 

## Functional Test Samples

 - ApplicationParts - minimalistic functional sample testing whether controllers are found correctly when registered from external assemblies. Uses manual `Startup` configuration and [NUnit](https://github.com/nunit/dotnet-test-nunit).
 - NoStartup - minimalistic functional sample showing how to use the testing library without any globally configured `Startup` class. Uses [MSTest](https://blogs.msdn.microsoft.com/visualstudioalm/2016/09/01/announcing-mstest-v2-framework-support-for-net-core-1-0-rtm/).