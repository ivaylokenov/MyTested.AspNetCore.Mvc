# When I run a test, I receive a type initializer exception without any descriptive message

`MyTested.AspNetCore.Mvc` uses a static constructor to bootstrap and prepare the test application. If your configuration is not valid, `System.TypeInitializerException` may be thrown. Depending on your test runner, you may not see a descriptive and helpful message.

In order to see more details about the problem, run the test in `Debug` mode and analyse the message of the `InnerException`.

<img src="/images/troubleshoot/typeinitexception.jpg" alt="Debugging Type Initializer Exception" />