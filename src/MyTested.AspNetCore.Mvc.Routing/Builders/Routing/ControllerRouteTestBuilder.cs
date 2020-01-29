namespace MyTested.AspNetCore.Mvc.Builders.Routing
{
    using System.Linq.Expressions;
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
        /// <param name="actionCallExpression">Method call expression indicating the expected resolved action.</param>
        public ControllerRouteTestBuilder(
            RouteTestContext testContext,
            LambdaExpression actionCallExpression)
            : base(testContext, actionCallExpression)
            => this.ActionCallExpression = actionCallExpression;

        public LambdaExpression ActionCallExpression { get; private set; }
    }
}
