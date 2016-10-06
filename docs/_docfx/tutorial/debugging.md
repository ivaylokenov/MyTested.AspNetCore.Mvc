# Debugging Failed Tests

In this section we will learn how easy is to debug failing tests.

## Friendly error messages

Let's see how nice and friendly error message My Tested ASP.NET Core MVC provides on a failed test. Go to the **"ManageControllerTest"** and change the redirect action:

```c#
.ToAction(nameof(ManageController.LinkLogin))
```

Run the test and you will be provided with a detailed error message showing exactly what has failed:

```
When calling RemoveLogin action in ManageController expected redirect result to have 'LinkLogin' action name, but instead received 'ManageLogins'.
```

We can see in the above message that the redirect action is actually **"ManageLogins"** so let's return that value and try something else. Change the **"Message"** route value property to **"Error"**:

```c#
.ToAction(nameof(ManageController.ManageLogins))
.ContainingRouteValues(new { Error = ManageController.ManageMessageId.Error });
```

Run the test again and you should see:

```
When calling RemoveLogin action in ManageController expected redirect result route values to have entry with 'Error' key and the provided value, but such was not found.
```

The library tells us that there is no **"Error"** key in the redirect route value dictionary. Now bring back the **"Message"** key to make the test pass again.

## Debugging the failing action

If the provided error messages are not enough to diagnose why the test fails, you can always use the good old C# debugger. Put a break point on the action method:

<img src="/images/tutorial/actiondebugging.jpg" alt="Debugging actions" />

Then click with the right mouse button on the failing test and select **"Debug Selected Tests"**:

<img src="/images/tutorial/debugselectedtests.jpg" alt="Debug through the test explorer" />

You know the drill from here on! :)

## Section summary

In this section we learned how helpful and developer-friendly is My Tested ASP.NET Core MVC with failed tests. But enough about failures and errors. Let's dive into the [Controllers](/tutorial/controllers.html) testing!