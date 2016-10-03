# Getting Started

First things first - we need a test assembly! Open the [Music Store solution](https://raw.githubusercontent.com/ivaylokenov/MyTested.AspNetCore.Mvc/development/docs/_docfx/tutorial/MusicStore-Tutorial.zip), add **"test"** folder and create a new .NET Core class library called **"MusicStore.Test"** in it.

<img src="/images/tutorial/createtestproject.jpg" alt="Create .NET Core test assembly" />

Delete the auto-generated **"Class1.cs"** file and open the **"project.json"** to configure the test runner:

 - Remove the version of the project. Test assemblies do not need it
 - Add **"netcoreapp1.0"** and **"net451"** to the supported frameworks and remove the **"netstandard1.6"** one. We want to run our tests for both the full .NET Framework and the .NET Core
 - Add **"dotnet-test-xunit"**, **"xunit"**, **"MyTested.AspNetCore.Mvc"** and **"MusicStore"** as dependencies
 - Set **"xunit"** to be the **"testRunner"** of the project
 
Your **"project.json"** file should look like this:

```json
{
  "dependencies": {
    "dotnet-test-xunit": "2.2.0-*",
    "xunit": "2.2.0-*",
    "MyTested.AspNetCore.Mvc": "1.0.0",
    "MusicStore": "*"
  },

  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dotnet5.4",
      "dependencies": {
        "Microsoft.NETCore.App": {
          "version": "1.0.1",
          "type": "platform"
        }
      }
    },
    "net451": {}
  },

  "testRunner": "xunit"
}
```

You may need to change/update the versions of the listed packages with more recent ones.

Now let's write our first unit test. We will test the **"AddressAndPayment"** action in the **"CheckoutController"**. It is one of the most simplest actions possible - returns a default view no matter the HTTP request.

<img src="/images/tutorial/addressandpaymentactions.jpg" alt="Simple controller action returning default view" />

Add a **"Controllers"** folder in the test assembly and create a **"CheckoutControllerTest"** class in it. Add these usings:

```c#
using MusicStore.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;
```

The only thing we need to assert in the tested action is whether it returns a view, so let's write it:

```c#
[Fact]
public void AddressAndPayment_ShouldReturn_DefaultView()
    => MyMvc
        .Controller<CheckoutController>()
        .Calling(c => c.AddressAndPayment())
        .ShouldReturn()
        .View();
```

Note the static **"MyMvc"** class. It is the starting point of the fluent interface but that depends on the installed packages in the test assembly. More details will be provided later in the tutorial.

Since My Tested ASP.NET Core MVC provides a fluent API, tests can be written with only a single statement thus we can use expression-bodied functions (available since C# 6.0). Of course, if you prefer, you can always use the normal curly brackets, nobody is going to stop you from doing it (for now)! :)

This should be your unit test now:

<img src="/images/tutorial/firstunittest.jpg" alt="First unit test returning simple view" />

Let's build the solution and run the test.

<img src="/images/tutorial/nostartuperror.jpg" alt="First unit test fails because of missing TestStartup class" />

Surpise! The simplest test fails. This testing framework is a waste of time! :(

Joke! Don't leave yet! By default My Tested ASP.NET Core MVC requires a **"TestStartup"** file at the root of the test assembly so let's add one. Write the following code in it:

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

Now run the test again.

<img src="/images/tutorial/configjsonerror.jpg" alt="First unit test fails because of missing config.json" />

You expected that, right? You should not be surprised after the first fail at all... :(

You still here? Good! Now repeat after me and then everything will be explained to you (it's a promise)!

Go to the **"MusicStore"** project root, copy the **"config.json"** file and paste it at the root of the test project. 

Go to the **"project.json"** file at the test project and add the copied **"config.json"** file to the build output:

```json
"buildOptions": {
  "copyToOutput": [
    "config.json"
  ]
},
```

<img src="/images/tutorial/configjson.jpg" alt="Copied config.json from the web project" />

You may be a bit worried about the connection string in the **"config.json"** file. I would be, if I did not know it was not needed at all. You can change it to whatever you like so let's make it "Test Connection". If you want to feel even safer, you may change all the other options too. It's up to you.

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

Don't be too happy yet as there is a (not-so) small problem here. Visual Studio runs the discovered tests only for the first targeted framework, which in our case is **"netcoreapp1.0"**. But how to test for the other one - **"net451"**?

Go to the **"MusicStore.Test"** project folder and open a console terminal there. The easiest way is pressing "SHIFT + Right Mouse Button" somewhere in the window and then clicking "Open command window here".

<img src="/images/tutorial/terminal.jpg" alt="Console terminal at the root of the test project" />

Type **"dotnet test"** and hit **"Enter**".

<img src="/images/tutorial/net451fail.jpg" alt=".NET Framework test fails" />

Oh, well... This does not look good... Fail again! Why are you even doing this tutorial, you may wonder? :(

If you still decide to stick around - .NET Core runs our test fine but the full .NET framework fails because it cannot load correctly all the plugin dependencies My Tested ASP.NET Core MVC needs (more information available [HERE](/guide/plugins.html). 

Do not worry, this one is easy. Go to the test assembly's **"project.json"** file and set the **"preserveCompilationContext"** option under **"buildOptions"** to **"true"**:

```json
"buildOptions": {
  "preserveCompilationContext": true,
  "copyToOutput": [
    "config.json"
  ]
},
```

Go back to the console terminal and run **"dotnet test"** again.

<img src="/images/tutorial/firsttestpass.jpg" alt="First test passes" />

Oh, miracles! The test passes correctly without any loud and ugly errors! Oh, yeah, do you feel the happiness? This library is really DA BOMB!!! :) 

OK, back to that promise - the detailed explanation of all the fails. **Basically three things happened.**

**First**, My Tested ASP.NET Core MVC needs to resolve the services required by the different components in your web application - controllers, view components, etc. By default the test configuration needs a **"TestStartup"** class at the root of the test project from where it configures the global service provider. This is why we got an exception telling us we forgot to add it. Remember:

 - Each test project needs separate **"Startup"** class and bootstraps a separate test application and service provider. You may run different configurations in different test assemblies. 
 - Each test is run in a scoped service lifetime - during a test all scoped services will be resolved by using the same instance and for the next test, another instance will be provided. My Tested ASP.NET Core MVC uses this nice little feature to allow for easy and autonomous testing of storage providers like the **"DbContext"**, **"IMemoryCache"**, **"ViewDataDictionary"** and many more but more on that later in the tutorial.
 
Besides the default **"TestStartup"** configuration there are two other options the developer can use - fluent manual configuration and per test setup without any globally registered services. More information can be found [HERE](/guide/startuptypes.html).

**Second**, the test failed because we did not have the required **"config.json"** file. If you take a look at the **"Startup"** file in the web project, you may see that the constructor of the class has the following lines of code:

```c#
var builder = new ConfigurationBuilder()
	.SetBasePath(hostingEnvironment.ContentRootPath)
	.AddJsonFile("config.json")
```

The JSON file is not optional and since we inherit from the original web **"Startup"**, our **"TestStartup"** class runs the same code thus requiring the **"config.json"** file to be present. (Un)fortunately the base project directory will be the output directory of the test project and the test runner will search for the file there. While we may make the **"config.json"** optional, it may lead to unexpected behaviour and exceptions so our best option here is to copy the same file into the test project and change all important values with dummy ones. Copy-pasting is not a good practice but letting the tests touch and read the original application configuration values like database connection strings, security passwords and potentially others is even worse. Additionally, only copying the file is not enough for it to end up in the output directory so we need to add it explicitly in the test assembly's **"project.json"** configuration.

**Third**, as we noticed Visual Studio does not run the discovered tests for all specified frameworks. This is why we went to the console and tried running there. Unfortunately, the test failed for the **"net451"** framework. The reason is simple. The full framework did not save and store our project references and dependencies. This is why all the required test plugin classes could not be loaded when using "net451". By setting the **"preserveCompilationContext"** option to **"true"** the compiler will store the dependencies information into a file from where later during runtime it can be read successfully.

Well, all is well that ends well! While the **"Getting Started"** section of this tutorial may feel a bit "kaboom"-ish, it covers all the common failures and problems you may encounter while using My Tested ASP.NET Core MVC. From now on it is all unicorns and rainbows. Go to the [Testing Controllers](/tutorial/controllers.html) section and see for yourself! :)