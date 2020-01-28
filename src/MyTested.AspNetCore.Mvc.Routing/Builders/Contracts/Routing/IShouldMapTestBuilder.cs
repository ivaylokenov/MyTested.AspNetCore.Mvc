namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Routing
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public interface IShouldMapTestBuilder : IResolvedRouteTestBuilder
    {
        /// <summary>
        /// Tests whether the built route is resolved to the action with the provided name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToAction(string actionName);

        /// <summary>
        /// Tests whether the built route is resolved to the controller with the provided name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder ToController(string controllerName);

        /// <summary>
        /// Tests whether the built route is resolved to the action and controller with the provided names.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder To(string actionName, string controllerName);

        /// <summary>
        /// Tests whether the built route is resolved to the controller of the given type.
        /// </summary>
        /// <typeparam name="TController">Type of the expected controller.</typeparam>
        /// <returns>The <see cref="IAndResolvedRouteTestBuilder"/>.</returns>
        IAndResolvedRouteTestBuilder To<TController>()
            where TController : class;

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The <see cref="IControllerRouteTestBuilder{TController}"/>.</returns>
        IControllerRouteTestBuilder<TController> To<TController>(Expression<Action<TController>> actionCall)
            where TController : class;

        /// <summary>
        /// Tests whether the built route is resolved to the asynchronous action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved asynchronous action.</param>
        /// <returns>The <see cref="IControllerRouteTestBuilder{TController}"/>.</returns>
        IControllerRouteTestBuilder<TController> To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class;

        /// <summary>
        /// Tests whether the built route cannot be resolved.
        /// </summary>
        void ToNonExistingRoute();
    }
}
