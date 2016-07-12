namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Routes;
    using Builders.Routes;
    using Internal.Application;
    using Internal.TestContexts;

    public class MyRoutes : RouteTestBuilder
    {
        static MyRoutes()
        {
            TestApplication.TryInitialize();
        }

        public MyRoutes()
            : base(new RouteTestContext
            {
                Router = TestApplication.Router,
                Services = TestApplication.RouteServices
            })
        {
        }

        public static IRouteTestBuilder Configuration()
        {
            return new MyRoutes();
        }
    }
}
