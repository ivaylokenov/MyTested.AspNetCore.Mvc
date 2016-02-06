namespace MyTested.Mvc.Builders.Contracts.Routes
{
    using Microsoft.AspNet.Mvc;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public interface IShouldMapTestBuilder : IResolvedRouteTestBuilder
    {
        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller;

        /// <summary>
        /// Tests whether the built route cannot be resolved because of not allowed method.
        /// </summary>
        void ToNotAllowedMethod();

        /// <summary>
        /// Tests whether the built route cannot be resolved.
        /// </summary>
        void ToNonExistingRoute();
    }
}
