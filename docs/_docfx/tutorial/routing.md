# Routing

If you have a single route mapping (for example the default one), it will be not hard for you to validate and assert whether all controllers and actions resolve correctly. However, when your application gets bigger and bigger, and you start to map different kinds of routes and introduce various changes to them, it can be quite challenging and messy to guarantee their integrity. Here route testing comes in handy.

## Validating controllers and actions

Go to the **"MusicStore.Test.csproj"** file and add the **"MyTested.AspNetCore.Mvc.Routing"** dependency:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Caching" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Attributes" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Options" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.Routing package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Routing" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Session" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewComponents" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewData" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

Create **"Routing"** folder at the root of the test project and add **"HomeRouteTest"** class in it. We will start with something easy and validate the **"Error"** action in **"HomeController"**:

```c#
public IActionResult Error()
{
    // action code skipped for brevity
}
```

The simplest route test possible:

```c#
[Fact]
public void GetErrorActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap("/Home/Error")
        .To<HomeController>(c => c.Error());
```

My Tested ASP.NET Core MVC gets the routing configuration from the provided **"TestStartup"** class. Note that the route testing will not fire any application middleware components, but it will run the MVC filters. It is validating whether the MVC router will select the correct controller and action based on the HTTP request data. Works with both conventional and attribute routing. Additionally, the testing framework uses the same services ASP.NET Core uses, so if you make any customizations to the route selection process, it will not interfere with the assertions logic and tests should still validate your mappings.

## Validating route values

We will now going to validate route values next to controllers and actions. The **"AddToCart"** action in the **"ShoppingCartController"** looks perfect for that purpose:

```c#
public async Task<IActionResult> AddToCart(int id)
{
    // action code skipped for brevity
}
```

Create **"ShoppingCartRouteTest"** class and add the following test:

```c#
[Fact]
public void GetAddToCartActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap("/ShoppingCart/AddToCart/1")
        .To<ShoppingCartController>(c => c.AddToCart(1, CancellationToken.None));
```

Query strings are also easy. Let's test the **"Browse"** action in the **"StoreController"**:

```c#
public async Task<IActionResult> Browse(string genre)
{
    // action code skipped for brevity
}
```

Create **"StoreRouteTest"** class and add the following test:

```c#
[Fact]
public void GetBrowseActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap("/Store/Browse?genre=HipHop")
        .To<StoreController>(c => c.Browse("HipHop"));
```

And if you change **"HipHop"** with **"Rock"**, for example, you will see the following error message:

```text
Expected route '/Store/Browse' to contain route value with 'genre' key and the provided value but the value was different.
```

## Ignoring route values

Some action parameters do not have to be tested. They come from the service provider, not the MVC routers. Let's take a look at the **"Index"** action in the **"HomeController"**:

```c#
public async Task<IActionResult> Index(
    [FromServices] MusicStoreContext dbContext,
    [FromServices] IMemoryCache cache)
{
    // action code skipped for brevity
}
```

We do not want to test the **"MusicStoreContext"** and the **"IMemoryCache"** action parameters. Ignoring them is a piece of cake - just use the helper method **"With.Any"** wherever you want to skip assertion:

```c#
[Fact]
public void GetIndexActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap("/Home")
        .To<HomeController>(c => c.Index(
            With.Any<MusicStoreContext>(), // <---
            With.Any<IMemoryCache>())); // <---
```

**"With.Any"** can be used on any action parameter expected in a route test.

## Action filters

All of the above examples are using HTTP Get method and the provided path as request data to test with. However, without adding more specific information, some actions cannot be routed correctly. For example **"RemoveFromCart"** in the **"ShoppingCartController"**:

```c#
[HttpPost]
public async Task<IActionResult> RemoveFromCart(
    int id,
    CancellationToken requestAborted)
{
    // action code skipped for brevity
}
```

The following test will fail right away:

```c#
[Fact]
public void PostRemoveFromCartActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap("/ShoppingCart/RemoveFromCart/1")
        .To<ShoppingCartController>(c => c.RemoveFromCart(
            1,
            With.Any<CancellationToken>()));
```

We are testing with HTTP Get request while the action is restricted only for HTTP Post ones. Let's fix the issue:

```c#
[Fact]
public void PostRemoveFromCartActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap(request => request // <---
            .WithMethod(HttpMethod.Post)
            .WithLocation("/ShoppingCart/RemoveFromCart/1"))
        .To<ShoppingCartController>(c => c.RemoveFromCart(
            1,
            With.Any<CancellationToken>()));
```

This way we are explicitly setting the request to have HTTP Post method making the routing match the specified controller, action and route value.

However, running the test produces the following error:

```text
Expected route '/ShoppingCart/RemoveFromCart/1' to match RemoveFromCart action in ShoppingCartController but action could not be invoked because of the declared filters. You must set the request properties so that they will pass through the pipeline.
```

As it was mentioned earlier, route testing executes the MVC action filters (even the globally registered ones in your **"Startup"** class). The **"RemoveFromCart"** action is defined with a **"ValidateAntiForgeryToken"** attribute, and we need to change our test a bit in order for it to pass. The anti-forgery token features of the library are located in the **"MyTested.AspNetCore.Mvc.ViewFeatures"** package, so let's install it:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Caching" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Attributes" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Options" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Routing" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Session" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewComponents" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewData" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.ViewFeatures package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewFeatures" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

Let's update the test to make it pass:

```c#
[Fact]
public void PostRemoveFromCartActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap(request => request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/ShoppingCart/RemoveFromCart/1")
            .WithAntiForgeryToken()) // <---
        .To<ShoppingCartController>(c => c.RemoveFromCart(
            1,
            With.Any<CancellationToken>()));
```

*Keep in mind that if you register the **"AutoValidateAntiforgeryTokenAttribute"** globally, you will still need to call **"WithAntiForgeryToken"** on your route tests.*

Another commonly used action filter you need to be aware of in your route tests is the **"Authorize"** attribute. 

Let's test a **"CheckoutController"** action, because all actions in that controller are for authorized users only. Create a **"CheckoutRouteTest"** class and add the following code in it:

```c#
[Fact]
public void GetAddressAndPaymentActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap(request => request
            .WithLocation("/Checkout/AddressAndPayment")
            .WithUser()) // <---
        .To<CheckoutController>(c => c.AddressAndPayment());
```

By calling **"WithUser"**, we are providing the router all the information it needs to match and validate the action. 

Sometimes we need to provide a user with a specific role or authorization policy. Take a look at the **"StoreManagerController"** in the **"Admin"** area:

```c#
[Area("Admin")]
[Authorize("ManageStore")]
public class StoreManagerController : Controller
{
    // controller code skipped for brevity
}
```

This controller is authorized only for the users who have a **"ManageStore"** claim with a value of "Allowed". This policy is defined in the web application's **"Startup"** class:

```c#
// class code skipped for brevity

// Configure Auth
services.AddAuthorization(options =>
{
    options.AddPolicy(
        "ManageStore",
        authBuilder =>
        {
            authBuilder.RequireClaim("ManageStore", "Allowed");
        });
});

// class code skipped for brevity
```

Let's test that route! In **"Routing"**, create an **"Admin"** folder. In it create a **"StoreManagerRouteTest"** class, add the necessary usings, and write the following test:

```c#
[Fact]
public void GetIndexActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap(request => request
            .WithLocation("/Admin/StoreManager")
            .WithUser(user => user
                .WithClaim("ManageStore", "Allowed"))) // <---
        .To<StoreManagerController>(c => c.Index());
```

Action filters covered! :)

## Model binding

Besides route values, you can also assert that all request properties (like its body for example) are bound to the action parameters and models. For example, the form fields in the HTTP Post overload of the **"Login"** action in the **"AccountController"**:

```c#
[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
{
    // action code skipped for brevity
}
```

The login view model should come from the request form so we may decide to ignore it by using **"With.Any"** or provide it like so:

```c#
[Fact]
public void PostLoginActionShouldBeRoutedSuccessfully()
    => MyRouting
        .Configuration()
        .ShouldMap(request => request
            .WithMethod(HttpMethod.Post)
            .WithLocation("/Account/Login?returnUrl=Test")
            .WithFormFields(new // <---
            {
                Email = "test@test.com",
                Password = "123456",
                RememberMe = "true"
            })
            .WithAntiForgeryToken())
        .To<AccountController>(c => c.Login(
            new LoginViewModel
            {
                Email = "test@test.com",
                Password = "123456",
                RememberMe = true
            },
            "Test"));
```

*Note that the **"RememberMe"** property value is provided as a string. This is the correct way because HTTP requests hold form fields in a simple text format. If you by mistake provide it as a C# boolean value, you will receive an error.*

The **"WithFormFields"** method call does some magic behind the scenes and it's just a shorter way to write:

```c#
.WithFormField("Email", "test@test.com")
.WithFormField("Password", "123456")
.WithFormField("RememberMe", "true"))
```

## JSON body

The **"Music Store"** web application does not have any JSON-based model binding, but it is not hard to test with one:

```c#
MyRouting
    .Configuration()
    .ShouldMap(request => request
        .WithMethod(HttpMethod.Post)
        .WithLocation("/My/Action")
        .WithJsonBody(@"{""MyNumber"":1,""MyString"":""MyText""}"))
    .To<MyController>(c => c.Action(
        new MyModel
        {
            MyNumber = 1,
            MyString = "MyText"
        }));
```

There is also an anonymous object overload:

```c#
MyRouting
    .Configuration()
    .ShouldMap(request => request
        .WithMethod(HttpMethod.Post)
        .WithLocation("/My/Action")
        .WithJsonBody(new
        {
            MyNumber = 1,
            MyString = "MyText"
        }))
    .To<MyController>(c => c.Action(
        new MyModel
        {
            MyNumber = 1,
            MyString = "MyText"
        }));
```

It may seem a bit strange at first, but My Tested ASP.NET Core MVC serializes the anonymous object to JSON string, attach it to the HTTP request body as a stream and pass it to the routing system.

Of course, you can always choose to ignore model binding and just assert controllers and actions:

```c#
MyRouting
    .Configuration()
    .ShouldMap(request => request
        .WithMethod(HttpMethod.Post)
        .WithLocation("/My/Action"))
    .To<MyController>(c => c.Action(With.Any<MyModel>()));
```

## Section summary

Still not convinced about the route testing and its capabilities? Check this [ultimate crazy model binding test](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/blob/development/test/MyTested.AspNetCore.Mvc.Routing.Test/BuildersTests/RoutingTests/RouteTestBuilderTests.cs#L766) which asserts JSON body, route values, query string parameters, form fields and headers at the same time. I hope no one writes such actions, though...

We are almost at the finish line. Next section will cover various test [Helpers](/tutorial/helpers.html) which do not fall within a particular tutorial section.
