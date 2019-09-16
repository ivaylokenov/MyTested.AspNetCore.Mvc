# Exceptions

Usually, a controller action should not throw any exceptions but rather return some result. However, that depends on the application configuration, conventions, and architecture. Let's dive deeper!

## Asserting thrown exceptions

Currently the **"Music Store"** application handles most of its potential bad requests, but there is a possible exception in the **"AddToCart"** action located in the **"ShoppingCartController"**. More specifically, this code:

```c#
var addedAlbum = await DbContext.Albums
    .SingleAsync(album => album.AlbumId == id);
```

If the database does not contain the provided **"id"**, our application will throw an exception. Let's assert that. Create **"ShoppingCartControllerTest"** class, add the necessary usings and write the following test:

```c#
[Fact]
public void AddToCartShouldThrowExceptionWithInvalidId()
    => MyController<ShoppingCartController>
        .Instance()
        .Calling(c => c.AddToCart(1, CancellationToken.None))
        .ShouldThrow() // <--
        .AggregateException()
        .ContainingInnerExceptionOfType<InvalidOperationException>()
        .WithMessage()
        .Containing("Sequence contains no elements");
```

Since we do not add any entries to the scoped in-memory database, the test should pass without any problem. With it we are validating that the action throws **"AggregateException"** with inner **"InvalidOperationException"** and a message containing **"Sequence contains no elements"**. Next to the **"AggregateException"**, there is the normal **"Exception"** call, which asserts non-asynchronous errors.

## Uncaught exceptions

Let's see what happens if the action throws an exception, and we try to validate a normal action result, for example:

```c#
[Fact]
public void AddToCartShouldThrowExceptionWithInvalidId()
    => MyController<ShoppingCartController>
        .Instance()
        .Calling(c => c.AddToCart(1))
        .ShouldReturn()
        .View();
```

As you may expect, we receive a nice and descriptive error message:

```text
When calling AddToCart action in ShoppingCartController expected no exception but AggregateException (containing InvalidOperationException with 'Sequence contains no elements' message) was thrown without being caught.
```

## Section summary

And kids... that's how we assert thrown exceptions! :)

Now let's revisit our [Options](/tutorial/options.html) testing!
