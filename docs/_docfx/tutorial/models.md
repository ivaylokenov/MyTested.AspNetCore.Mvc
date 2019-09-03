# Models

In this section we will learn how to validate the model state and assert the action result models.

## Model state validation

The **"ModelStateDictionary"** class is commonly used in a typical MVC application when the request method is **"POST"**. In the previous section we wrote this test specifying the model state error manually:

```c#
var model = new ChangePasswordViewModel
{
    OldPassword = "OldPass",
    NewPassword = "NewPass",
    ConfirmPassword = "NewPass"
};

MyController<ManageController>
    .Instance()
    .WithSetup(controller => controller.ModelState
        .AddModelError("TestError", "TestErrorMessage"))
    .Calling(c => c.ChangePassword(model))
    .ShouldReturn()
    .View()
    .AndAlso()
    .ShouldPassForThe<ViewResult>(viewResult => Assert.Same(model, viewResult.Model));
```

To skip the manual arrange of the model state dictionary, we can use the built-in validation in My Tested ASP.NET Core MVC. It is quite easy to do - the testing framework will validate all models passed as action parameters by default. If you examine the **"ChangePasswordViewModel"**, you will notice the two required properties - **"OldPassword"** and **"NewPassword"**. So, if we provide our action method with null values for these two model properties, My Tested ASP.NET Core MVC will validate them by using the registered services in the **"TestStartup"** class we create earlier. So let's change the view model, remove the **"WithSetup"** call, and run the test again:

```c#
var model = new ChangePasswordViewModel(); // <---

MyController<ManageController>
    .Instance()
    .Calling(c => c.ChangePassword(model))
    .ShouldReturn()
    .View()
    .AndAlso()
    .ShouldPassForThe<ViewResult>(viewResult => Assert.Same(model, viewResult.Model));
```

The test still passes but it we examine the **"ChangePassword"** action, we will notice that the same result is returned from the action when the password fails to change. In other words - we are not sure which case is asserted with the above test. We can easily fix the issue by using the following line:

```c#
.ShouldPassForThe<Controller>(controller => Assert.Equal(2, controller.ModelState.Count))
```

However, there is always a better way! Go to the **"MusicStore.Test.csproj"** file and add **"MyTested.AspNetCore.Mvc.ModelState"** as a dependency:

```xml
<!-- Other ItemGroups -->

<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
  <!-- Other ItemGroups -->
```

Besides the **"ShouldReturn"**, there is another very helpful method - **"ShouldHave"**. With **"ShouldHave"** you can test different kinds of components after the action has been invoked. For example, we want to check whether the model state has become invalid, so we need to add:

```c#
.ShouldHave()
.InvalidModelState()
```

These lines will validate whether the model state is invalid after the action call. By providing an integer to the method, you can specify the total number of expected validation errors. Moreover, you can easily combine them with **"ShouldReturn"** by using **"AndAlso"**:

```c#
var model = new ChangePasswordViewModel();

MyController<ManageController>
    .Instance()
    .Calling(c => c.ChangePassword(model))
    .ShouldHave() // <---
    .InvalidModelState(withNumberOfErrors: 2)
    .AndAlso() // <---
    .ShouldReturn()
    .View()
    .AndAlso()
    .ShouldPassForThe<ViewResult>(viewResult => Assert.Same(model, viewResult.Model));
```

Rebuild the project and run the test to see it pass successfully. If you change the **"InvalidModelState"** call to **"ValidModelState"**, you can see a nice descriptive error message:

```text
When calling ChangePassword action in ManageController expected to have valid model state with no errors, but it had some.
```

If you want to be more specific, the fluent API allows testing for specific model state errors:

```c#
.ShouldHave()
.ModelState(modelState => modelState
    .ContainingError(nameof(ChangePasswordViewModel.OldPassword))
    .ThatEquals("The Current password field is required.")
    .AndAlso()
    .ContainingError(nameof(ChangePasswordViewModel.NewPassword))
    .ThatEquals("The New password field is required.")
    .AndAlso()
    .ContainingNoError(nameof(ChangePasswordViewModel.ConfirmPassword)))
.AndAlso()
```

There is a better way to test for specific model state errors, but more on that later (as always in this tutorial). :)

Most of the time you will want to run the validation during the action call. However, if you don't want for some reason, add **"MyTested.AspNetCore.Mvc.DataAnnotations"** to your **"MusicStore.Test.csproj"** file and call ""*WithoutValidation*"" for the tested controller.

## Action result models

To test action result models, you need to add **"MyTested.AspNetCore.Mvc.Models"** as a dependency of the test assembly:

```xml
<!-- Other ItemGroups -->

<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.ActionResults" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Controllers.Views" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.Models" Version="2.2.0" />
    <PackageReference Include="MyTested.AspNetCore.Mvc.ModelState" Version="2.2.0" />
    
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
  <!-- Other ItemGroups -->
```

By adding the above package, you will add another set of useful extension methods for all action results returning a model object. First, remove this line from the **"ChangePassword"** test:

```c#
.ShouldPassForThe<ViewResult>(viewResult => Assert.Same(model, viewResult.Model));
```

Good! Now back to those extension methods - the first one is **"WithNoModel"**, which asserts for exactly what it says (as every other method in the library, of course) - whether the action result returns a 'null' model. Add the method after the **"View"** call, rebuild the project and run the test to see what happens:

```c#
var model = new ChangePasswordViewModel();

MyController<ManageController>
    .Instance()
    .Calling(c => c.ChangePassword(model))
    .ShouldHave()
    .InvalidModelState()
    .AndAlso()
    .ShouldReturn()
    .View(view => view.WithNoModel());
```

We should receive error message with no doubt - our action returns the same model after all:

```text
When calling ChangePassword action in ManageController expected to not have a view model but in fact such was found.
```

Obviously, this is not the method we need. :)

From here on we have two options - testing the whole model for deep equality or testing just parts of the model (the ones we care the most).

Let's see the deep equality:

```c#
.View(v => v.WithModel<ChangePasswordViewModel>(model));
```

Since we expect the action to return the same view model as the one provided as an action parameter, we just pass it to the **"WithModel"** method, and it will be validated for us. Note that this test will also work:

```c#
var model = new ChangePasswordViewModel
{
    ConfirmPassword = "TestValue"
};

MyController<ManageController>
    .Instance()
    .Calling(c => c.ChangePassword(model))
    .ShouldHave()
    .InvalidModelState()
    .AndAlso()
    .ShouldReturn()
    .View(view => 
        view.WithModel<ChangePasswordViewModel>(
            new ChangePasswordViewModel {
                    ConfirmPassword = "TestValue" })
        );
```

Although the models are not pointing to the same instance, My Tested ASP.NET Core MVC will validate them by comparing their properties deeply. It works perfectly with interfaces, collections, generics, comparables, nested models and [many more object types](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/blob/development/test/MyTested.AspNetCore.Mvc.Abstractions.Test/UtilitiesTests/ReflectionTests.cs#L426). 

Although it is cool and easy to use the deep equality assertion, most of the time it is not worth it. Models which have a lot of data may need a lot of code to make the test pass successfully. Supporting such huge objects is also a tedious task.

Introducing the last model assertion options - **"WithModelOfType"** and **"Passing"**. These two methods combined can give you enough flexibility to test only what you need from the model object. **"WithModelOfType"** allows you to test only for the type of the action result model so let's use it instead of **"WithModel"**:

```c#
.View(view => view.WithModelOfType<ChangePasswordViewModel>());
```

The test will pass if you run it, but you still need to assert whether the returned model was the same as the parameter one. Luckily, the **"Passing"** method takes a delegate which tests the action result model, allowing you to be as specific in your assertions as you see fit:

```c#
var model = new ChangePasswordViewModel();

MyController<ManageController>
    .Instance()
    .Calling(c => c.ChangePassword(model))
    .ShouldHave()
    .InvalidModelState()
    .AndAlso()
    .ShouldReturn()
    .View(view => view.WithModelOfType<ChangePasswordViewModel>()
                      .Passing(viewModel => viewModel == model));
```

Aaaand... our work here is done (this time for real)! :)

## Section summary

This section covered an important part of the testing framework. Almost all actions in ASP.NET Core MVC use various types of request or response models. You will see more examples for model assertions in the tutorial but for now let's move to one of biggest components of the typical web application - the [Database](/tutorial/database.html)!