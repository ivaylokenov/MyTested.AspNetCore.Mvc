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

Go to the **"MusicStore.Test.csproj"** file and add **"MyTested.AspNetCore.Mvc.Http"** as a dependency:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.Http package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

This package will provide you with additional methods - two of them are **"WithHttpContext"** and **"WithHttpRequest"**. We will use the second one - it provides a fast way to set up every single part of the HTTP request.

Go to the **"CheckoutControllerTest"** class and add the following test:

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
        .InvalidModelState()
        .AndAlso()
        .ShouldReturn()
        .View(result => result
            .WithModel(With.Default<Order>()));
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

By default tests do not have an authenticated user identity. Write this theory in the **"CheckoutControllerTest"**, run it, and see for yourself:

```c#
[Theory]
[InlineData(1)]
public void CompleteShouldReturnViewWithCorrectIdWithFoundOrderForTheUser(int orderId)
    => MyController<CheckoutController>
        .Instance()
        .WithData(new Order
        {
            OrderId = orderId,
            Username = "TestUser"
        })
        .Calling(c => c.Complete(
            From.Services<MusicStoreContext>(),
            orderId))
        .ShouldReturn()
        .View(result => result
            .WithModel(orderId));
```

It fails. Obviously, we need an authenticated user to test this action. We can attach it to the **"HttpContext"** but let's make it easier. Head over to the **"MusicStore.Test.csproj"** file again and add **"MyTested.AspNetCore.Mvc.Authentication"**:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
	<!-- MyTested.AspNetCore.Mvc.Authentication package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

**"WithUser"** method will be added to the fluent API. You can use it to set identifier, username, roles, claims, and identities. But for now call it empty like this:

```c#
[Theory]
[InlineData(1)]
public void CompleteShouldReturnViewWithCorrectIdWithFoundOrderForTheUser(int orderId)
    => MyController<CheckoutController>
        .Instance()
        .WithUser()
        .WithData(new Order
        {
            OrderId = orderId,
            Username = "TestUser"
        })
        .Calling(c => c.Complete(
            From.Services<MusicStoreContext>(),
            orderId))
        .ShouldReturn()
        .View(result => result
            .WithModel(orderId));
```

You will receive a passing test because the default authenticated user has **"TestId"** identifier and **"TestUser"** username. Change the order's **"Username"** property to **"MyTestUser"** and you will need to provide the username of the identity in order to make the test pass again:

```c#
[Theory]
[InlineData(1, "MyTestUser")]
public void CompleteShouldReturnViewWithCorrectIdWithFoundOrderForTheUser(
    int orderId,
    string username)
    => MyController<CheckoutController>
        .Instance()
        .WithUser(user => user // <---
            .WithUsername(username))
        .WithData(new Order
        {
            OrderId = orderId,
            Username = username
        })
        .Calling(c => c.Complete(
            From.Services<MusicStoreContext>(),
            orderId))
        .ShouldReturn()
        .View(result => result
            .WithModel(orderId));
```

Of course, we also need to test the result when the order is not for the currently authenticated user. In this case, we need to assert the **"Error"** view, but to do it fluently open the **"MusicStore.Test.csproj"** file again and add **"MyTested.AspNetCore.Mvc.Views.ActionResults"** package:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.DependencyInjection" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.EntityFrameworkCore" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Http" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

The **"Views.ActionResults"** package contains additional useful extension methods for view related results.

Now add this test and it should pass:

```c#
[Theory]
[InlineData(1)]
public void CompleteShouldReturnErrorViewWithInvalidOrderForTheUser(int orderId)
    => MyController<CheckoutController>
        .Instance()
        .WithUser(user => user
            .WithUsername("InvalidUser"))
        .WithData(new Order
        {
            OrderId = orderId,
            Username = "MyTestUser"
        })
        .Calling(c => c.Complete(
            From.Services<MusicStoreContext>(),
            orderId))
        .ShouldReturn()
        .View(result => result
            .WithName("Error")); // <---
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
        .View(result => result
            .WithName("~/Views/Shared/AccessDenied.cshtml"));
```

The **"HttpResponse"** method allows assertions of every part of the HTTP response - body, headers, cookies, etc. For example, if you add this line:

```c#
.ShouldHave()
.HttpResponse(response => response
    .ContainingHeader("InvalidHeader") // <---
    .WithStatusCode(HttpStatusCode.OK))
.AndAlso()
```

You will receive a nice little error message (as always):

```text
When calling AccessDenied action in HomeController expected HTTP response headers to contain header with 'InvalidHeader' name, but such was not found.
```

Cool! :)

## Section summary

Well, these were easier than the last section's test services. While the request testing is more suitable for other components, authentication plays a significant role in the actions' logic.

You have learned quite a lot. Let's take a break from the code and learn more about the [Licensing](/tutorial/licensing.html) of the testing framework.
