namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Pipeline;
    using Builders.Contracts.Routing;
    using Builders.Pipeline;
    using Builders.Routing;
    using Internal.Results;
    using Internal.TestContexts;

    /// <summary>
    /// Contains extension methods for <see cref="IControllerRouteTestBuilder{TController}"/>.
    /// </summary>
    public static class ControllerRouteTestBuilderPipelineExtensions
    {
        /// <summary>
        /// Allows the route test to continue the assertion chain on the matched controller action.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IControllerRouteTestBuilder{TController}"/> type.</param>
        /// <returns>Test builder of <see cref="IWhichControllerInstanceBuilder{TController}"/> type.</returns>
        public static IWhichControllerInstanceBuilder<TController> Which<TController>(
            this IControllerRouteTestBuilder<TController> builder)
            where TController : class
        {
            var actualBuilder = (ControllerRouteTestBuilder<TController>)builder;

            var actionCall = actualBuilder.ActionCallExpression;

            return new WhichControllerInstanceBuilder<TController>(new ControllerTestContext
            {
                ComponentConstructionDelegate = () => null,
                MethodCall = actionCall
            });
        }

        /// <summary>
        /// Allows the route test to continue the assertion chain on the matched controller action.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IControllerRouteTestBuilder{TController}"/> type.</param>
        /// <param name="controllerInstanceBuilder">Builder for creating the controller instance.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        public static IActionResultTestBuilder<MethodResult> Which<TController>(
            this IControllerRouteTestBuilder<TController> builder,
            Action<IControllerInstanceBuilder<TController>> controllerInstanceBuilder)
            where TController : class
        {
            return null;
        }
    }
}
