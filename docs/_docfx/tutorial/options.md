# Options

Remember this code?

```c#
.WithServices(services => services
    .WithSetupFor<IOptions<AppSettings>>(settings => settings
        .Value.CacheDbResults = false))
```

In this section we are going to improve it with the built-in options setup methods.

## Options configuration setup

Go to the **"MusicStore.Test.csproj"** file and add **"MyTested.AspNetCore.Mvc.Options"** as a dependency of the test project:

```xml
<!-- Other ItemGroups -->

<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.2.0" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Helpers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Options" Version="2.2.0" />

    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
</ItemGroup>

<!-- Other ItemGroups -->
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

Well, this was easy. In fact, it's the easiest part of this tutorial. Let's move to [Session & Cache](/tutorial/sessioncache.html) where we will use the options setup one more time.
