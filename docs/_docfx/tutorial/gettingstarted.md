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

// Make screenshot directly on the project.json with highlighted the two files, remove the database connection string
