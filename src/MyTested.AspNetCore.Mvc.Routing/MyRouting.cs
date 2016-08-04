namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Routing;
    using Builders.Routing;
    using Internal.Application;
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
        }

        public static IRouteTestBuilder Configuration()
        {
            return new MyRouting();
        }
    }
}
