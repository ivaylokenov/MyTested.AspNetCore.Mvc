namespace MyTested.AspNetCore.Mvc.Builders.Routing
{
    using Contracts.Routing;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing controller route details.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public class ControllerRouteTestBuilder<TController>
        : ShouldMapTestBuilder,
        IControllerRouteTestBuilder<TController>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerRouteTestBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="RouteTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerRouteTestBuilder(RouteTestContext testContext) 
            : base(testContext)
        {
        }
    }
}
