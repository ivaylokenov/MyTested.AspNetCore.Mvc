# Attributes

This section will cover testing of attributes and their properties.

## Controller attributes

First, we need another package. Again?! 

Do not worry, later in this tutorial we are going to use the **"Universe"** package and this madness will end. The purpose here is to show you the different components of the testing framework.

Now, obey the rules and add **"MyTested.AspNetCore.Mvc.Controllers.Attributes"** to your **"MusicStore.Test.csproj"** project:

```xml
<!-- Other ItemGroups -->

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.0.1" />
    <PackageReference Include="Moq" Version="4.13.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Authentication" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
	<!-- MyTested.AspNetCore.Mvc.Controllers.Attributes package -->
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Attributes" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
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

Let's assert that our **"CheckoutController"** is decorated with the commonly used **"AuthorizeAttribute"**. Go to the **CheckoutControllerTest"** class and add the following test:

```c#
[Fact]
public void CheckoutControllerShouldHaveAuthorizeAttribute()
    => MyController<CheckoutController>
        .Instance()
        .ShouldHave()
        .Attributes(attributes => attributes // <---
            .RestrictingForAuthorizedRequests());
```

Simple as that. Additionally, in the the **"HomeControllerTest"** class, we can add:

```c#
[Fact]
public void HomeControllerShouldHaveNoAttributes()
    => MyController<HomeController>
        .Instance()
        .ShouldHave()
        .NoAttributes();
```

Of course, if you change **"NoAttributes"** to **"Attributes"**, you will receive an error:

```text
When testing HomeController was expected to have at least 1 attribute, but in fact none was found.
```

## Action attributes

Action attributes are not different. Let's test the **"RemoveAlbumConfirmed"** action in the **"StoreManagerController"**:

```c#
[HttpPost, ActionName("RemoveAlbum")]
public async Task<IActionResult> RemoveAlbumConfirmed(
	[FromServices] IMemoryCache cache,
	int id,
	CancellationToken requestAborted)
{
		
// action code skipped for brevity
```

We need to test the **"HttpPost"** and **"ActionName"** attributes:

```c#
[Fact]
public void RemoveAlbumConfirmedShouldHaveCorrectAttributes()
    => MyController<StoreManagerController>
        .Instance()
        .Calling(c => c.RemoveAlbumConfirmed(
            With.No<IMemoryCache>(),
            With.No<int>(),
            With.No<CancellationToken>()))
        .ShouldHave()
        .ActionAttributes(attributes => attributes // <---
            .RestrictingForHttpMethod(HttpMethod.Post)
            .ChangingActionNameTo("RemoveAlbum"));
```

Working like a charm! :)

## Custom attributes

Sometimes you will have custom attributes which are not available in the fluent testing API. For example, you may have noticed that there is no method to test the **"ValidateAntiForgeryTokenAttribute"**". Actually, it's in the **"ViewFeatures"** package, but you don't know that! :)

Let's see an example and test the HTTP Post **"Login"** action in the **"AccountController"**. It has these three attributes - **"HttpPost"**, **"AllowAnonymous"**, **"ValidateAntiForgeryToken"**. For the latter you can use the **"ContainingAttributeOfType"** method:

```c#
[Fact]
public void LoginShouldHaveCorrectAttributes()
    => MyController<AccountController>
        .Instance()
        .Calling(c => c.Login(
            With.Default<LoginViewModel>(),
            With.No<string>()))
        .ShouldHave()
        .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .AllowingAnonymousRequests()
            .ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>()); // <---
```

The action is still invoked in this test, so we need to provide a non-null value for the **"LoginViewModel"** parameter. A better approach for testing action attributes (without having to specify the parameters) will be available in the next major release of the library. :)

Sometimes you may want to test specific property values of the attribute. You can use the **"PassingFor"** method:

```c#
[Fact]
public void StoreManagerControllerShouldHaveCorrectAttributes()
    => MyController<StoreManagerController>
        .Instance()
        .ShouldHave()
        .Attributes(attributes => attributes
            .SpecifyingArea("Admin")
            .PassingFor<AuthorizeAttribute>(authorize => authorize // <---
                .Policy == "ManageStore"));
```

## Section summary

We saw how easy it is to assert and validate all kinds of controller and action attributes. But enough about them - in the next section we will cover thrown [Exceptions](/tutorial/exceptions.html).