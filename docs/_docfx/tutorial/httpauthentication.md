# HTTP & Authentication

This section will cover HTTP related testing and user identity authentication.

## HTTP requests

Sometimes we need to process the HTTP request in the controller action. Take a look at the HTTP Post overload of the **"AddressAndPayment"** action in the **"CheckoutController"**:

```c#
// action code skipped for brevity

var formCollection = await HttpContext.Request.ReadFormAsync();

try
{
	if (string.Equals(formCollection["PromoCode"].FirstOrDefault(), PromoCode,
		StringComparison.OrdinalIgnoreCase) == false)
	{
		return View(order);
	}
	
// action code skipped for brevity
```

The action reads the form and checks for an input named **"PromoCode"**. If it does not equal **"FREE"**, the action returns its view with the same order provided by the form. Let's test this logic!

Go to the **"project.json"** file and add **"MyTested.AspNetCore.Mvc.Http"** as a dependency:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0",
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.Http": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MusicStore": "*"
},
```

This package will provide you with additional methods - two of them are **"WithHttpContext"** and **"WithHttpRequest"**. We will use the second one - it provides a fast way to set up every single part of the HTTP request. 

Go to the **"CheckoutControllerTest"** and add the following test:

```c#
[Fact]
public void AddressAndPaymentShouldRerurnViewWithInvalidPostedPromoCode()
    => MyController<CheckoutController>
        .Instance()
        .WithHttpRequest(request => request // <---
            .WithFormField("PromoCode", "Invalid"))
        .Calling(c => c.AddressAndPayment(
            From.Services<MusicStoreContext>(),
            With.Default<Order>(),
            CancellationToken.None))
        .ShouldHave()
        .ValidModelState()
        .AndAlso()
        .ShouldReturn()
        .View()
        .WithModel(With.Default<Order>());
```

We have successfully tested that with an invalid promo code in the request form, our action should return the same view with the proper model. The **"WithHttpRequest"** method allows you to add form fields, files, headers, body, cookies and more. We will see more of it when we cover route testing.

## Authentication

Now let's take a look at the **"Complete"** action in the same controller:

```c#
// action code skipped for brevity

var userName = HttpContext.User.Identity.Name;

bool isValid = await dbContext.Orders.AnyAsync(
	o => o.OrderId == id &&
	o.Username == userName);

if (isValid)
{
	return View(id);
}
else
{
	return View("Error");
}

// action code skipped for brevity
```

By default tests do not have an authenticated user identity. Write this one in the **"CheckoutControllerTest"**, run it and see for yourself:

```c#
[Fact]
public void CompleteShouldReturnViewWithCorrectIdWithFoundOrderForTheUser()
    => MyController<CheckoutController>
        .Instance()
        .WithDbContext(db => db
            .WithEntities(entities => entities.Add(new Order
            {
                OrderId = 1,
                Username = "TestUser"
            })))
        .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
        .ShouldReturn()
        .View()
        .WithModel(1);
```

It fails. Obviously, we need an authenticated user to test this action. We can attach it to the **"HttpContext"** but let's make it easier. Head over to the **"project.json"** file again and add **"MyTested.AspNetCore.Mvc.Authentication"**:

```json
"dependencies": {
  "dotnet-test-xunit": "2.2.0-*",
  "xunit": "2.2.0-*",
  "Moq": "4.6.38-*",
  "MyTested.AspNetCore.Mvc.Authentication": "1.0.0", // <---
  "MyTested.AspNetCore.Mvc.Controllers": "1.0.0",
  "MyTested.AspNetCore.Mvc.DependencyInjection": "1.0.0",
  "MyTested.AspNetCore.Mvc.EntityFrameworkCore": "1.0.0",
  "MyTested.AspNetCore.Mvc.Http": "1.0.0",
  "MyTested.AspNetCore.Mvc.ModelState": "1.0.0",
  "MyTested.AspNetCore.Mvc.Models": "1.0.0",
  "MyTested.AspNetCore.Mvc.ViewActionResults": "1.0.0",
  "MusicStore": "*"
},
```

**"WithAuthenticatedUser"** method will be added to the fluent API. You can use it to set identifier, username, roles, claims, and identities. But for now call it empty like this:

```c#
[Fact]
public void CompleteShouldReturnViewWithCorrectIdWithFoundOrderForTheUser()
    => MyController<CheckoutController>
        .Instance()
        .WithAuthenticatedUser() // <---
        .WithDbContext(db => db
            .WithEntities(entities => entities.Add(new Order
            {
                OrderId = 1,
                Username = "TestUser"
            })))
        .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
        .ShouldReturn()
        .View()
        .WithModel(1);
```

You will receive a passing test because the default authenticated user has **"TestId"** identifier and **"TestUser"** username. Change the order **"Username"** property to **"MyTestUser"** and you will need to provide the username of the identity in order to make the test pass again:

```c#
[Fact]
public void CompleteShouldReturnViewWithCorrectIdWithFoundOrderForTheUser()
    => MyController<CheckoutController>
        .Instance()
        .WithAuthenticatedUser(user => user // <---
            .WithUsername("MyTestUser"))
        .WithDbContext(db => db
            .WithEntities(entities => entities.Add(new Order
            {
                OrderId = 1,
                Username = "MyTestUser"
            })))
        .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
        .ShouldReturn()
        .View()
        .WithModel(1);
```

Of course, we also need to test the result when the order is not for the currently authenticated user. In this case, we need to return the **"Error"** view:

```c#
[Fact]
public void CompleteShouldReturnErrorViewWithInvalidOrderForTheUser()
    => MyController<CheckoutController>
        .Instance()
        .WithAuthenticatedUser(user => user
            .WithUsername("InvalidUser"))
        .WithDbContext(db => db
            .WithEntities(entities => entities.Add(new Order
            {
                OrderId = 1,
                Username = "MyTestUser"
            })))
        .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
        .ShouldReturn()
        .View("Error");
```

## HTTP Response

Sometimes we may manipulate the HTTP response directly in the controller action. For example, to add a custom header. The Music Store web application does not have such logic, but we can take any action and validate whether it returns 200 (OK) status code just for the sake of seeing the syntax.

Create a **"HomeControllerTest"** class and add the following test:

```c#
[Fact]
public void AccessDeniedShouldReturnOkStatusCodeAndProperView()
    => MyController<HomeController>
        .Instance()
        .Calling(c => c.AccessDenied())
        .ShouldHave()
        .HttpResponse(response => response // <---
            .WithStatusCode(HttpStatusCode.OK))
        .AndAlso()
        .ShouldReturn()
        .View("~/Views/Shared/AccessDenied.cshtml");
```

The **"HttpResponse"** method allows assertions of every part of the HTTP response - body, headers, cookies, etc. For example, if you add this line:

```c#
.ContainingHeader("InvalidHeader")
```

You will receive a nice little error message (as always):

```text
When calling AccessDenied action in HomeController expected HTTP response headers to contain header with 'InvalidHeader' name, but such was not found.
```

Cool! :)

## Section summary

Well, these were easier than the last section's test services. While the request testing is more suitable for other components, authentication plays a significant role in the actions' logic.

If you followed the tutorial strictly, you should have reached the free trial version limitations of My Tested ASP.NET Core MVC. Let's take a break from the code and learn more about the [Licensing](/tutorial/licensing.html) of the testing framework.