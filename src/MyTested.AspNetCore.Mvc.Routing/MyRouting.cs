namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Routing;
    using Builders.Routing;
    using Internal.Application;
    using Internal.Configuration;
    using Internal.TestContexts;

    public class MyRouting : RouteTestBuilder
    {
        static MyRouting()
        {
            TestApplication.TryInitialize();
        }

        public MyRouting()
            : base(new RouteTestContext
            {
                Router = TestApplication.Router,
                Services = TestApplication.RoutingServices
            })
        {
            if (TestApplication.Configuration().General().NoStartup())
            {
                throw new InvalidOperationException("Testing routes without a Startup class is not supported. Set the 'General.NoStartup' option in the test configuration ('testconfig.json' file by default) to 'true' and provide a Startup class.");
            }
        }

        public static IRouteTestBuilder Configuration()
        {
            return new MyRouting();
        }
    }
}
