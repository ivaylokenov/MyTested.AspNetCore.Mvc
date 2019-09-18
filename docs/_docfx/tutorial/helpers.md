# Various Helpers

This section will cover various test helpers which make writing tests faster and easier! :)

## The Helpers package

You may add the **"MyTested.AspNetCore.Mvc.Helpers"** package which adds extension methods to the fluent API. But let's do something else. I believe you got tired of all these packages so we will delete them and add only the one that rules them all:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
	<!-- MyTested.AspNetCore.Mvc.Universe package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Universe" Version="2.2.0" />
    <PackageReference Include="xunit" Version="2.4.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.0" />
  </ItemGroup>

<!-- Other ItemGroups -->
```

The **"Universe"** package combines all other packages.

One of the helpers is allowing us to assert controller action results with a single method call instead of multiple ones. For example, we may have these lines of code:

```c#
.ShouldReturn()
.View(result => result
    .WithModel(model);
```

They can be written like this:

```c#
.ShouldReturn()
.View(model)
```

We have such test in **"AccountControllerTest"**:

```c#
[Fact]
public void LoginShouldReturnViewWithSameModelWithInvalidLoginViewModel()
{
    var model = new LoginViewModel
    {
        Email = "invalid@invalid.com",
        Password = "invalid"
    };

    var redirectUrl = "/Test/Url";

    MyController<AccountController>
        .Instance()
        .Calling(c => c.Login(model, redirectUrl))
        .ShouldHave()
        .ModelState(modelState => modelState
            .ContainingError(string.Empty)
            .ThatEquals("Invalid login attempt."))
        .AndAlso()
        .ShouldReturn()
        .View(model); // <---
}
```

## Global MyMvc class

The **"MyTested.AspNetCore.Mvc"** package introduced another static class named **"MyMvc"**. Instead of these:

```c#
MyController<TestController>.Instance()
MyViewComponent<TestViewComponent>.Instance()
MyRouting.Configuration()
```

You can use these:

```c#
MyMvc.Controller<TestController>()
MyMvc.ViewComponent<TestViewComponent>()
MyMvc.Routing()
```

It is up to you!

## Model state expression-based assertions

For example, in the **"ManageControllerTest"** we may use string-based model state assertions for the **"ChangePassword"** action:

```c#
.Calling(c => c.ChangePassword(model))
.ShouldHave()
.ModelState(modelState => modelState
    .ContainingError(nameof(ChangePasswordViewModel.OldPassword))
    .ThatEquals("The Current password field is required.")
    .AndAlso()
    .ContainingError(nameof(ChangePasswordViewModel.NewPassword))
    .ThatEquals("The New password field is required.")
    .AndAlso()
    .ContainingNoError(nameof(ChangePasswordViewModel.ConfirmPassword)))

// instead of .InvalidModelState(withNumberOfErrors: 2)
```

But you may also use expression-based ones:

```c#
.Calling(c => c.ChangePassword(model))
.ShouldHave()
.ModelState(modelState => modelState
    .For<ChangePasswordViewModel>()
    .ContainingErrorFor(m => m.OldPassword)
    .ThatEquals("The Current password field is required.")
    .AndAlso()
    .ContainingErrorFor(m => m.NewPassword)
    .ThatEquals("The New password field is required.")
    .AndAlso()
    .ContainingNoErrorFor(m => m.ConfirmPassword))
```

## Expression-based route values

Instead of testing for redirects by using multiple method calls like in the **"ManageControllerTest"**:

```c#
.ShouldReturn()
.Redirect(result => result
    .ToAction(nameof(ManageController.ManageLogins))
    .ContainingRouteValues(new { Message = ManageController.ManageMessageId.Error }));
```

You may use a single expression-based assertion call:

```c#
.ShouldReturn()
.Redirect(result => result
    .To<ManageController>(c => c
        .ManageLogins(ManageController.ManageMessageId.Error)));
```

## Resolving route data

Let's test the **"AddressAndPayment"** action in the **"CheckoutController"**. We will validate for correct redirection:

```c#
[Theory]
[InlineData(1, 1, "TestCart")]
public void AddressAndPaymentShouldRerurnRedirectWithValidData(
    int albumId, 
    int orderId,
    string cartSession)
    => MyController<CheckoutController>
        .Instance()
        .WithHttpRequest(request => request
            .WithFormField("PromoCode", "FREE"))
        .WithSession(session => session
            .WithEntry("Session", cartSession))
        .WithUser()
        .WithData(data => data
            .WithEntities(entities =>
            {
                var album = new Album { AlbumId = albumId, Price = 10 };
                var cartItem = new CartItem
                {
                    Count = 1,
                    CartId = cartSession,
                    AlbumId = albumId,
                    Album = album
                };
                entities.Add(album);
                entities.Add(cartItem);
            }))
        .WithoutValidation()
        .Calling(c => c.AddressAndPayment(
            From.Services<MusicStoreContext>(),
            new Order { OrderId = orderId },
            With.No<CancellationToken>()))
        .ShouldReturn()
        .Redirect(result => result
            .To<CheckoutController>(c => c
                .Complete(With.Any<MusicStoreContext>(), orderId)));
```

Running this test will give us the following strange error message:

``` When calling AddressAndPayment action in CheckoutController expected redirect result to have resolved location to '/Checkout/Complete/1', but in fact received '/Home/Complete/1'.
```

The problem is that the request path is empty which makes the action route data being invalid. For that reason, we are receiving wrong redirection location. The fix is easy - just call **"WithRouteData"**:

```c#
[Theory]
[InlineData(1, 1, "TestCart")]
public void AddressAndPaymentShouldRerurnRedirectWithValidData(
    int albumId,
    int orderId,
    string cartSession)
    => MyController<CheckoutController>
        .Instance()
        .WithHttpRequest(request => request
            .WithFormField("PromoCode", "FREE"))
        .WithSession(session => session
            .WithEntry("Session", cartSession))
        .WithUser()
        .WithRouteData() // <---
        .WithData(data => data
            .WithEntities(entities =>
            {
                var album = new Album { AlbumId = albumId, Price = 10 };
                var cartItem = new CartItem
                {
                    Count = 1,
                    CartId = cartSession,
                    AlbumId = albumId,
                    Album = album
                };
                entities.Add(album);
                entities.Add(cartItem);
            }))
        .WithoutValidation()
        .Calling(c => c.AddressAndPayment(
            From.Services<MusicStoreContext>(),
            new Order { OrderId = orderId },
            With.No<CancellationToken>()))
        .ShouldReturn()
        .Redirect(result => result
            .To<CheckoutController>(c => c
                .Complete(With.Any<MusicStoreContext>(), orderId)));
```

The method call will resolve all the route values for you. The reason it is not done by default is because of performance considerations. You may manually provide route data values if you need:

```c#
.WithRouteData(new { controller = "Checkout" })
```

The above issue may appear again when testing for the **"Accepted"**, **"Created"** and **"Redirect"** action results. In some cases, the testing framework may catch the error and suggest you a fix:

``` Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.
```

For example, the test bellow will show the above message, if **"WithRouteData"** is not called because the **"ExternalLogin"** action uses **"IUrlHelper"**.

```c#
[Fact]
public void ExternalLoginShouldReturnCorrectResult()
    => MyController<AccountController>
        .Instance()
        .WithRouteData()
        .Calling(c => c.ExternalLogin("TestProvider", "TestReturnUrl"))
        .ShouldReturn()
        .Challenge();
```

## Additional attribute validations

Some packages expose additional attribute validations. For example, adding the **"Microsoft.AspNetCore.Mvc.ViewFeatures"**, will add the option to test the **"AntiForgeryTokenAttribute"**. Instead of:

```c#
.ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>()
```

You can use:

```c#
.ValidatingAntiForgeryToken()
```

## Section summary

With this section, we finished with the most important parts of the fluent assertion API. Few non-syntax related topics to read and you are free to go. Go to the [Organizing Tests](/tutorial/organizingtests.html) section to see the various ways you can write your tests!
