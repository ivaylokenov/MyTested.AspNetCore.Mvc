# Extension Methods

In this final section of the tutorial we will learn how to extend the functionality of My Tested ASP.NET Core MVC.

## Extending existing test builders

Let's add an extension method which allows as to assert collection models like:

```c#
.WithModelOfType<List<Album>>()
.Passing(albums => albums.Count == 6)
```

But on a single line:

```c#
.WithCollectionModelOfType<Albums>(albums => albums.Count == 6)
```

If we take a look at the **"WithModelOfType"** signature, we will see it is an extension method to the **"IBaseTestBuilderWithResponseModel"** interface:

```c#
public static IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>(this IBaseTestBuilderWithResponseModel builder);
```

We will extend the same interface so that all action results with models are extended. Create a folder **"Extensions"** in the test project and add **"ResponseModelExtensions"** class containing the code below.

```c#
using MyTested.AspNetCore.Mvc.Builders.Base;
using MyTested.AspNetCore.Mvc.Builders.Contracts.Base;
using MyTested.AspNetCore.Mvc.Exceptions;
using MyTested.AspNetCore.Mvc.Utilities;
using MyTested.AspNetCore.Mvc.Utilities.Extensions;
using System;
using System.Collections.Generic;

public static class ResponseModelExtensions
{
    public static IBaseTestBuilderWithComponent WithCollectionModelOfType<TModel>(
        // base test builder we are extending
        this IBaseTestBuilderWithResponseModel builder,
        // optional predicate the model should pass
        Func<ICollection<TModel>, bool> predicate = null)
    {
        // cast to the actual class behind the interface
        var actualBuilder = (BaseTestBuilderWithResponseModel)builder;
        // helper method validating the model type
        var modelCollection = actualBuilder.GetActualModel<ICollection<TModel>>();

        // execute the predicate if exists
        if (predicate != null && !predicate(modelCollection))
        {
            // get the current test context
            var testContext = actualBuilder.TestContext;

            // throw exception for invalid predicate
            throw new ResponseModelAssertionException(string.Format(
                "When calling {0} in {1} expected response model collection of {2} to pass the given predicate, but it failed.",
                testContext.MethodName,
                testContext.Component.GetName(),
                typeof(TModel).ToFriendlyTypeName()));
        }

        // return the same test builder
        return actualBuilder;
    }
}
```

Let's break it down and explain the most important parts of this extension method:

- **"IBaseTestBuilderWithComponent"** is a base interface containing **"ShouldPassFor"** methods allowing you to continue the fluent API.
- **"actualBuilder"** is a variable holding the actual class behind the **"IBaseTestBuilderWithResponseModel"** interface. The class' name is the same as the interface but without the 'I' in front of it. After the casting you will receive more functionality you can use - methods like the **"GetActualModel"** used above. Their purpose is to help you execute the test but not to be part of the actual fluent API.
- The **"TestContext"** is part of every single test builder class. It contains information about the currently executed test. For example in the scope of a controller test, the **"Component"** property will contain the controller instance and the **"MethodName"** property will contain the name of the tested action. More information available [HERE](/guide/testcontext.html).
- The **"GetName"** and **"ToFriendlyTypeName"** extension methods can be used to format various display names.

## Using existing methods

Let's add new testing functionality based on existing methods. For example instead of this call:

```c#
.ShouldHave()
.Attributes(attributes => attributes
    .SpecifyingArea("Admin"))
```

We remove the magic string:

```c#
.ShouldHave()
.Attributes(attributes => attributes
    .SpecifyingAdminArea())
```

We need to extend the **"IControllerActionAttributesTestBuilder<TAttributesTestBuilder>"** interface. Add **"ControllerActionAttributeExtensions"** class with the following code in it:

```c#
using MyTested.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc.Builders.Contracts.Attributes;

public static class ControllerActionAttributeExtensions
{
    public static TAttributesTestBuilder SpecifyingAdminArea<TAttributesTestBuilder>(
        this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> builder)
        where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        => builder.SpecifyingArea("Admin");
}
```

## Final words

With this section we conclude the tutorial successfully! The final source code with all tests is available [HERE](https://raw.githubusercontent.com/ivaylokenov/MyTested.AspNetCore.Mvc/master/docs/files/MusicStore-Tutorial-Final.zip). But before we say goodbye let's rebuild and rerun all tests again just for the sake of it. Do the same with the CLI by running **"dotnet test"**. Everything passes? Good!

Hopefully, you fell in love with My Tested ASP.NET Core MVC and if not - ideas and suggestions are [always welcome](https://mytestedasp.net/#contact)!

Thank you for reading the whole tutorial and have fun testing your web applications! :)
