# Getting Started

In this section we will learn how to configure My Tested ASP.NET Core MVC and get familiar with all the small issues we may encounter in the process.

## Prepare test assembly

First things first - we need a test assembly! Open the [Music Store solution](https://raw.githubusercontent.com/ivaylokenov/MyTested.AspNetCore.Mvc/master/docs/files/MusicStore-Tutorial.zip), add **"test"** folder and create a new .NET Core class library called **"MusicStore.Test"** in it.

<img src="/images/tutorial/createtestproject.jpg" alt="Create .NET Core test assembly" />

Delete the auto-generated **"Class1.cs"** file and open the **"MusicStore.Test.csproj"** to configure the test runner:

Use NuGet package manager to install these packages:
 - [MyTested.AspNetCore.Mvc](https://www.nuget.org/packages/MyTested.AspNetCore.Mvc/)
 - [MyTested.AspNetCore.Mvc.Universe](https://www.nuget.org/packages/MyTested.AspNetCore.Mvc.Universe/)
 - [xunit](https://www.nuget.org/packages/xunit/)
 - [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio/)

After that you must add reference to the **"MusicStore.Web"** project.

Now your **"MusicStore.Test.csproj"** file should look like this*:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" Version="2.2.6" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Universe" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\MusicStore.Web\MusicStore.Web.csproj" />
  </ItemGroup>

</Project>
```

*You may need to change/update the versions of the listed packages with more recent ones.

## Our first test

Now let's write our first unit test. We will validate the **"AddressAndPayment"** action in the **"CheckoutController"**. It is one of the simplest actions possible - returns a default view no matter the HTTP request.

```c#
// controller code skipped for brevity

public IActionResult AddressAndPayment()
{
	return View();
}

// controller code skipped for brevity
```

Add a **"Controllers"** folder in the test assembly and create a **"CheckoutControllerTest"** class in it. Add these usings:

```c#
using MusicStore.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;
```

The only thing we need to assert in the tested action is whether it returns a view, so let's write it:

```c#
[Fact]
public void AddressAndPaymentShouldReturnDefaultView()
    => MyMvc
        .Controller<CheckoutController>()
        .Calling(c => c.AddressAndPayment())
        .ShouldReturn()
        .View();
```

Note the static **"MyMvc"** class. It is the starting point of the fluent interface, but that depends on the installed packages in the test assembly. More details will be provided later in the tutorial.

Since My Tested ASP.NET Core MVC provides a fluent API, tests can be written with only a single statement. Therefore we can use expression-bodied functions (available in C# 6.0). Of course, if you prefer, you can always use the regular curly brackets, nobody is going to stop you from doing it (for now)! :)

## "TestStartup" class

Let's build the solution and run the test.

<img src="/images/tutorial/nostartuperror.jpg" alt="First unit test fails because of missing TestStartup class" />

Surprise! The simplest test fails. This testing framework is a waste of time! :(

Joke! Don't leave yet! By default, My Tested ASP.NET Core MVC requires a **"TestStartup"** file at the root of the test assembly so let's add one. Write the following code in it:

```c#
namespace MusicStore.Test
{
    using Microsoft.AspNetCore.Hosting;

    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }
    }
}
```

You may have noticed the constructor of the **"CheckoutController"**. It is not an empty one. My Tested ASP.NET Core MVC uses the registered services from the **"TestStartup"** class to resolve all dependencies and instantiate the controller. We will get into more details about the test service provider later in this tutorial.

## Web configuration

Now run the test again.

<img src="/images/tutorial/configjsonerror.jpg" alt="First unit test fails because of missing config.json" />

You were expecting that, right? You should not be surprised after the first fail at all... :(

Still here? Good! Now repeat after me and then everything will be explained to you (it's a promise)!

Go to the **"MusicStore"** project root, copy the **"config.json"** file and paste it at the root of the test project.

After than you must swith **"Copy To Output Directory"** property of **"config.json"** file to **"Copy if newer"** or manualy put this on the **"MusicStore.Test.csproj"** file :
```xml
  <!--Other ItemGroups -->
  
  <ItemGroup>
    <Content Update="config.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <!--Other ItemGroups -->
```

<img src="/images/tutorial/configjson.jpg" alt="Copied config.json from the web project" />

You may be a bit worried about the connection string in the **"config.json"** file. I would be if I didn't know it was not needed at all. You may change its value to whatever you like so let's make it "Test Connection". If you want to feel even safer, you may change all the other options too. It's up to you.

Your **"config.json"** file should look like this:

```json
{
  "AppSettings": {
    "SiteTitle": "ASP.NET MVC Music Store",
    "CacheDbResults": true
  },
  "DefaultAdminUsername": "Administrator@test.com",
  "DefaultAdminPassword": "YouShouldChangeThisPassword1!",
  "Data": {
    "DefaultConnection": {
      "ConnectionString": "Test Connection"
    }
  }
}
```

Now run the test again in Visual Studio and... oh, miracle, it passes! :)

## Understanding the details

OK, back to that promise - the detailed explanation for all the different fails.

**Basically two things happened.**

**First**, My Tested ASP.NET Core MVC needs to resolve the services required by the different components in your web application - controllers, view components, etc. By default, the testing framework is configured to need a **"TestStartup"** class at the root of the test project from where it prepares the global service provider. For this reason, we got an exception telling us we need to add the **"TestStartup"** class. Remember:

 - Each test project requires separate **"Startup"** class and bootstraps a separate test application and service provider. You may run different configurations in different test assemblies. 
 - Each test runs in a scoped service lifetime - during a test all scoped services will be resolved by using the same instances and for the next test, other instances will be provided. My Tested ASP.NET Core MVC uses this nice little feature to run smooth and autonomous tests for storage providers like the **"DbContext"**, **"IMemoryCache"**, **"ViewDataDictionary"** and many others but more on that later in the tutorial.
 
Besides the default **"TestStartup"** configuration there are two other options the developer can use - manual fluent configuration and per test setup without any globally registered services. More information can be found [HERE](/guide/startuptypes.html).

**Second**, the test failed because we did not have the required **"config.json"** file. If you take a look at the **"Startup"** file in the web project, you may see that the constructor of the class has the following lines of code:

```c#
var builder = new ConfigurationBuilder()
	.SetBasePath(hostingEnvironment.ContentRootPath)
	.AddJsonFile("config.json")
```

The JSON file is not optional, and since we inherit from the original web **"Startup"**, our **"TestStartup"** class runs the same code thus requiring the **"config.json"** file to be present. (Un)fortunately, the base project directory will be the output directory of the test project, and the test runner will search for the file there. We may make the **"config.json"** optional, but it may lead to unexpected behavior and exceptions in our web application, so our best option here is to copy the same file into the test project and change all important values with dummy ones. Copy-pasting is not a good practice but letting the tests touch and read the original application configuration values like database connection strings, security passwords, and potentially others is even worse. Additionally, only copying the file is not enough for it to end up in the output directory, so we need to add it explicitly in the test assembly's **"MusicStore.Test.csproj"** configuration.

## Error messages

To finish this section let's make the test fail because of an invalid assertion just to see what happens. Instead of testing for **"View"**, make it assert for any other action result. **"BadRequest"**, for example:

```c#
[Fact]
public void AddressAndPaymentShouldReturnDefaultView()
    => MyController<CheckoutController>
        .Instance()
        .Calling(c => c.AddressAndPayment())
        .ShouldReturn()
        .BadRequest();
```

Run the test, and you will see a nice descriptive error message from My Tested ASP.NET Core MVC:

```text
MyTested.AspNetCore.Mvc.Exceptions.InvocationResultAssertionException :
 When calling AddressAndPayment action in CheckoutController expected result to be BadRequestResult, but instead received ViewResult.
```

Of course, you should undo the change and return the **"View"** call (unless you want a failing test during the whole tutorial but that's up to you again). :)

## Section summary

Well, all is well that ends well! While the **"Getting Started"** section of this tutorial may feel a bit "KABOOM"-ish, it covers all the common failures and problems you may encounter while starting to use My Tested ASP.NET Core MVC. From now on it is all unicorns and rainbows. Go to the [Packages](/tutorial/packages.html) section and see for yourself! :)