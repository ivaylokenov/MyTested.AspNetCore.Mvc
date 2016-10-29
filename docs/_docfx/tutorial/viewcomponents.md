# View Components

In this section we are going to stop asserting controllers (finally) and concentrate on **"View Components"**. Typical MVC application written with separation of concerns in mind should have plenty of them. Before we begin, we need the **"MyTested.AspNetCore.Mvc.ViewComponents"** package:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Authentication": "1.0.0",
  "MyTested.AspNetCore.Mvc.Caching": "1.0.0",
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0",
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.Http": "1.0.0",
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.Options": "1.0.0",
  "MyTested.AspNetCore.Mvc.Session": "1.0.0",
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MyTested.AspNetCore.Mvc.ViewComponents": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.ViewData": "1.0.0",
  "MusicStore": "*"
},
```

Then we need to create a **"ViewComponents"** folder at the root of our test project and add **"CartSummaryComponentTest"** class in it. We are going to assert the **"CartSummaryComponent"** component.

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

We need session cart ID and items in the database for the shopping cart. Let's create a helper method for generating the entities:

```c#
private static IEnumerable<CartItem> GetCartItems(string cartId, string albumTitle)
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
	.WithDbContext(db => db // <---
		.WithEntities(entities => entities
			.AddRange(GetCartItems("TestCart", "TestAlbum"))))
```

## Act

After arranging all objects we need for the view component, we have to invoke it by using the **"InvokedWith"** method:

```c#
MyViewComponent<CartSummaryComponent>
	.Instance()
	.WithSession(session => session
		.WithEntry("Session", "TestCart"))
	.WithDbContext(db => db
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
	.WithDbContext(db => db
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

As with all tests with My Tested ASP.NET Core MVC, if you have and invalid value, you will receive a friendly error message. Change **"Session"** to **"Cache"** and see for yourself:

```text
When invoking CartSummaryComponent expected view bag to have entry with 'CartCount' key and the provided value, but the value was different.
```

## Section Summary

View component testing provides the same API methods as the controller one as long as they are applicable. You should be seeing the big picture of the library now. The next section is interesting - the good old [Routing](/tutorial/routing.html) testing!