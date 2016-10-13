# Options

Remember this code?

```c#
.WithServices(services => services
    .WithSetupFor<IOptions<AppSettings>>(settings => settings
        .Value.CacheDbResults = false))
```

In this section we are going to improve it with the built-in options setup methods.

## Options configuration setup

Go to the **"project.json"** file and add **"MyTested.AspNetCore.Mvc.Options"** as a dependency of the test project:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Authentication": "1.0.0",
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0",
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.Http": "1.0.0",
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.Options": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MusicStore": "*"
},
```

Adding this package will automatically make all the options related services scoped.

Go to the unit test asserting the **"Details"** action in the **"StoreManagerControllerTest"** controller and change the following code:

```c#
.WithServices(services => services
    .WithSetupFor<IOptions<AppSettings>>(settings => settings
        .Value.CacheDbResults = false))
```

With this one:

```c#
.WithOptions(options => options
	.For<AppSettings>(settings => settings.CacheDbResults = false))
```

Much more readable! :)

Additionally, the **"TestStartup"** class no longer needs this call:

```c#
services.AddScoped<IOptions<AppSettings>, OptionsManager<AppSettings>>();
```

Our **"ConfigureTestServices"** should now contain the following:

```c#
public void ConfigureTestServices(IServiceCollection services)
{
	base.ConfigureServices(services);
	
	services.ReplaceLifetime<IMemoryCache>(ServiceLifetime.Scoped);

	services.ReplaceSingleton<SignInManager<ApplicationUser>>(sp => 
		MockProvider.SignInManager(sp.GetRequiredService<UserManager<ApplicationUser>>()));
}
```

## Section summary

Well, this was easy. In fact, it it is the easiest part of this tutorial. Let's move to [Session & Cache](/tutorial/sessioncache.html) where we will use the options setup one more time.