namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Routing;
    using Builders.Routing;
    using Internal;
    using Internal.Application;
    using Internal.Configuration;
    using Internal.TestContexts;

    /// <summary>
    /// Provides methods to specify an ASP.NET Core MVC route test case. 
    /// This assertion chain does not execute the filters pipeline.
    /// </summary>
    public class MyRouting : RouteTestBuilder
    {
        static MyRouting() => TestApplication.TryInitialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyRouting"/> class.
        /// This assertion chain does not execute the filters pipeline.
        /// </summary>
        public MyRouting(bool fullExecution = false)
            : base(new RouteTestContext
            {
                Router = TestApplication.Router,
                Services = TestApplication.RoutingServices,
                FullExecution = fullExecution
            })
        {
            if (ServerTestConfiguration.Global.GetGeneralConfiguration().NoStartup)
            {
                throw new InvalidOperationException(ExceptionMessages.RouteTestingUnavailable);
            }
        }

        /// <summary>
        /// Starts a route test. This assertion chain does not execute the filters pipeline.
        /// </summary>
        /// <returns>Test builder of <see cref="IRouteTestBuilder"/> type.</returns>
        public static IRouteTestBuilder Configuration() => new MyRouting();
    }
}
