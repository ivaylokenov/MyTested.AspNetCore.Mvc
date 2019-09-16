# View Components

In this section we are going to stop asserting controllers (finally) and concentrate on **"View Components"**. Typical MVC application written with separation of concerns in mind should have plenty of them. Before we begin, we need the **"MyTested.AspNetCore.Mvc.ViewComponents"** package:

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
    <PackageReference Include="MyTested.AspNetCore.Mvc.Session" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.ViewComponents package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewComponents" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ViewData" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

Then we need to create a **"ViewComponents"** folder at the root of our test project and add **"CartSummaryComponentTest"** class in it. We are going to assert the **"CartSummaryComponent"**.

## Arrange

Arranging the test is done in the same manner as it is with controllers. This is the code we want to test:

```c#
private MusicStoreContext DbContext { get; }

public async Task<IViewComponentResult> InvokeAsync()
{
    var cart = ShoppingCart.GetCart(DbContext, HttpContext);

    var cartItems = await cart.GetCartAlbumTitles();

    ViewBag.CartCount = cartItems.Count;
    ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

    return View();
}
```

We need session cart ID and items in the database for the shopping cart. Add a **"CartItemData"** class in the **"Data"** folder and write these lines in it:

```c#
using MusicStore.Models;
using System.Collections.Generic;
using System.Linq;

public class CartItemData
{
    public static IEnumerable<CartItem> GetMany(string cartId, string albumTitle)
    {
        var album = new Album { AlbumId = 1, Title = albumTitle };

        return Enumerable
            .Range(1, 10)
            .Select(n => new CartItem
            {
                AlbumId = 1,
                Album = album,
                Count = 1,
                CartId = cartId,
            });
    }
}
```

Starting the test is done from the **"MyViewComponent"** test:

```c#
[Fact]
public void InvokingTheComponentShouldReturnCorrectCartItems()
    => MyViewComponent<CartSummaryComponent>
        .Instance()
```

We need to add a cart ID in the session state:

```c#
MyViewComponent<CartSummaryComponent>
    .Instance()
    .WithSession(session => session // <---
        .WithEntry("Session", "TestCart"))
```

And database entities:

```c#
MyViewComponent<CartSummaryComponent>
    .Instance()
    .WithSession(session => session
        .WithEntry("Session", "TestCart"))
    .WithData(CartItemData.GetMany("TestCart", "TestAlbum")) // <---
```

## Act

After arranging all objects we need for the view component, we have to invoke it by using the **"InvokedWith"** method:

```c#
MyViewComponent<CartSummaryComponent>
    .Instance()
    .WithSession(session => session
        .WithEntry("Session", "TestCart"))
    .WithData(db => db
        .WithEntities(entities => entities
            .AddRange(GetCartItems("TestCart", "TestAlbum"))))
    .InvokedWith(vc => vc.InvokeAsync()) // <---
```

## Assert

Finally, we need to assert the **"ViewBag"** and the execution result. You already now how to do it:

```c#
MyViewComponent<CartSummaryComponent>
    .Instance()
    .WithSession(session => session
        .WithEntry("Session", "TestCart"))
    .WithData(db => db
        .WithEntities(entities => entities
            .AddRange(GetCartItems("TestCart", "TestAlbum"))))
    .InvokedWith(vc => vc.InvokeAsync())
    .ShouldHave() // <---
    .ViewBag(viewBag => viewBag
        .ContainingEntries(new
        {
            CartCount = 10,
            CartSummary = "TestAlbum"
        }))
    .AndAlso()
    .ShouldReturn() // <---
    .View();
```

Rebuild the project and run the test to see it pass! :)

As with all tests with My Tested ASP.NET Core MVC, if you have an invalid value, you will receive a friendly error message. Change **"Session"** to **"Cache"** and see for yourself:

```text
When invoking CartSummaryComponent expected view bag to have entry with 'CartCount' key and the provided value, but the value was different.
```

## Section Summary

View component testing provides the same API methods as the controller one as long as they are applicable. You should be seeing the big picture of the library now. The next section is interesting - the good old [Route](/tutorial/routing.html) testing!
