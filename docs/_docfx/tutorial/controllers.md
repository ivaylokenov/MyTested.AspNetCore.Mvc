# Controllers

In this section we will dive a bit deeper into controller testing. By understanding it, you will get familiar with the fundamentals of My Tested ASP.NET Core MVC and see how a lot of other components from a typical MVC web application are asserted in a similar manner. Of course we will use the classical AAA (Arrange, Act, Assert) approach.

## Arrange

Go to the **"ManageController"** again and analyse the **"ChangePassword"** action. You will notice that with invalid model state this action returns view result with the same model provided as a request parameter. Particularly, we want to test these lines of code:

```c#
if (!ModelState.IsValid)
{
	return View(model);
}
``` 

My Tested ASP.NET Core MVC provides a very easy way to arrange the model state, but we will ignore it for now. What we want is to add a model error to the action call manually. Go to the **"ManageControllerTest"** class, add the new test and start with the typical selection of a controller to test:

```c#
[Fact]
public void ChangePasswordShouldReturnViewWithSameModelWithInvalidModelState()
{
    MyController<ManageController>
        .Instance()
}
```

We will now examine three different ways to arrange the model state on the tested **""ManageController"**. Since the model state is part of the controller (action) context (see [HERE](https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs#L83)), you may instantiate one (after adding the **"Microsoft.AspNetCore.Mvc"** using) and provide it by using the **"WithControllerContext"** (**"WithActionContext"**) method:

```c#
var controllerContext = new ControllerContext();
controllerContext.ModelState.AddModelError("TestError", "TestErrorMessage");

MyController<ManageController>
    .Instance()
    .WithControllerContext(controllerContext)
```

Since the testing framework prepares for you everything it can before running the actual test case by using the test service provider, you may skip the instantiation and use the other overload of the method using an action delegate:

```c#
MyController<ManageController>
    .Instance()
    .WithControllerContext(context => context.ModelState
        .AddModelError("TestError", "TestErrorMessage"))
```

These are fine but the model state dictionary can be accessed directly from the controller itself, so we can just skip the whole **"ControllerContext"** class by using the **"WithSetup"** method:

```c#
MyController<ManageController>
    .Instance()
    .WithSetup(controller => controller.ModelState
        .AddModelError("TestError", "TestErrorMessage"))
```

As you can see the **"WithSetup"** method will come in handy wherever the fluent API does not provide a specific arrange method. As a side note - My Tested ASP.NET Core MVC provides an easy way to set up the model state dictionary, but we will cover it later in this tutorial.

Each one of these three ways for arranging the controller is fine but stick with the third option for now.

## Act

We need to act! In other words we need to call the action method. We do not need an actual request model to test the desired logic, so let's pass a null value as a parameter. Add this line to the test:

```c#
.Calling(c => c.ChangePassword(null))
```

You should be familiar with the **"Calling"** method from the previous sections. Again, if you prefer to be more expressive, you may use the **"With"** class:

```c#
.Calling(c => c.ChangePassword(With.No<ChangePasswordViewModel>()))
```

Well, this was easy! :)

## Assert

The final part of our test is asserting the action result. You should know how to assert a view result too, so add these to the test:

```c#
.ShouldReturn()
.View();
```

We now need to test the returned model. It should be the same as the one provided through the action parameter. If you look through the intellisense after the **"View"** call, you will not find anything related to models. The reason simple - model testing is available in a separate package which we will install in the next section.

For now we will use the tools we already have imported in our test project. Introducing the magical **"ShouldPassForThe<TWhateverYouLike>"** method! I know developers do not like magic code but this one is cool, I promise! :)

Add the following lines to the test:

```c#
.AndAlso()
.ShouldPassForThe<ViewResult>(viewResult => Assert.Null(viewResult.Model));
```

Now run the test and we are ready! :)

But before moving on with our lives, let's explain these two lines.

First - the **"AndAlso"** method. It is just there for better readability and expressiveness. It is available on various places in the fluent API but it actually does nothing most of the time. You may remove it from your code now, then recompile it and run the test again and it will still pass. Of course, it is up to you whether or not to use the **"AndAlso"** method but admit it - it is a nice little addition to the test! :)

Second - the magical **"ShouldPassForThe<ViewResult>"** call. To make sure it works correctly, let's change the **"Assert.Null"** to **"Assert.NotNull"** and run the test. It should fail loud and clear with the original **"xUnit"** message:

```
Assert.NotNull() Failure
```

Return back the **"Null"** call so that the test passes again. The **"ShouldPassForThe<TComponent>"** method obviously works. What is interesting here is that the generic parameter **"TComponent"** can be anything you like, as long it is recognised by My Tested ASP.NET Core MVC. Seriously, add the following to the test and run the test:

```c#
.ShouldReturn()
.View()
.AndAlso()
.ShouldPassForThe<Controller>(controller =>
{
    Assert.NotNull(controller);
    Assert.True(controller.ModelState.ContainsKey("TestError"));
})
.AndAlso()
.ShouldPassForThe<ViewResult>(viewResult => Assert.Null(viewResult.Model));
```

Of course the first **"ShouldPassForThe"** call does not make any sense at all but it proves that everything related to the test can be asserted by using the method. You may even put a break point into the action delegate and debug it, if you like.

I guess that you already know it, but if you put an invalid and unrecognisable type for the generic parameter, for example **"XunitProjectAssembly"**, you will receive an exception:

```
XunitProjectAssembly could not be resolved for the 'ShouldPassForThe<TComponent>' method call.
```

To continue, let's bring back the test to its last passing state:

```c#
[Fact]
public void ChangePasswordShouldReturnViewWithSameModelWithInvalidModelState()
{
	MyController<ManageController>
		.Instance()
		.WithSetup(controller => controller.ModelState
			.AddModelError("TestError", "TestErrorMessage"))
		.Calling(c => c.ChangePassword(With.No<ChangePasswordViewModel>()))
		.ShouldReturn()
		.View()
		.AndAlso()
		.ShouldPassForThe<ViewResult>(viewResult => Assert.Null(viewResult.Model));
}
```

We are still not asserting whether the view model is the same object as the provided method parameter. Let's change that by instantiating a **"ChangePasswordViewModel"** and test the action with it:

```c#
[Fact]
public void ChangePasswordShouldReturnViewWithSameModelWithInvalidModelState()
{
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
}
```

Our work here is done (for now)! :)

## Section summary

In this section we saw the AAA approach collaborating gracefully with My Tested ASP.NET Core MVC. However, I know you remember reading earlier about an easier way of arranging the model state and more fluent testing options for the view result models. You can learn about them in the [Models](/tutorial/models.html) section!