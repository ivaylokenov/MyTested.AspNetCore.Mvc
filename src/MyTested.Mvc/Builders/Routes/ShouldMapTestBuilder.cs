namespace MyTested.Mvc.Builders.Routes
{
    using System;
    using System.Linq.Expressions;
    using Contracts.Routes;
    using Utilities;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Routing;
    using Internal.Routes;
    using Exceptions;
    using Internal.Extensions;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public class ShouldMapTestBuilder : BaseRouteTestBuilder, IAndShouldMapTestBuilder, IAndResolvedRouteTestBuilder
    {
        private readonly RouteContext routeContext;

        private LambdaExpression actionCallExpression;
        private ResolvedRouteContext actualRouteInfo;
        private ExpressionParsedRouteContext expectedRouteInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldMapTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use for the route test.</param>
        /// <param name="location">URI location represented by string.</param>
        public ShouldMapTestBuilder(
            IRouter router,
            IServiceProvider serviceProvider,
            RouteContext routeContext)
            : base(router, serviceProvider)
        {
            this.routeContext = routeContext;
        }

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller
        {
            return this.ResolveTo<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller
        {
            return this.ResolveTo<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether the built route cannot be resolved because of not allowed method.
        /// </summary>
        public void ToNotAllowedMethod()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.MethodIsNotAllowed)
            {
                this.ThrowNewRouteAssertionException(
                    string.Format("not allow method '{0}'", this.routeContext.HttpContext.Request.Method),
                    actualInfo.IsResolved ? "in fact it was allowed" : actualInfo.UnresolvedError);
            }
        }

        /// <summary>
        /// Tests whether the built route cannot be resolved.
        /// </summary>
        public void ToNonExistingRoute()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (actualInfo.IsResolved)
            {
                this.ThrowNewRouteAssertionException(
                    "be non-existing",
                    string.Format("in fact it was {0}", "resolved successfully"));
            }
        }

        /// <summary>
        /// And method for better readability when building route HTTP request message.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IShouldMapTestBuilder And()
        {
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when building route tests.
        /// </summary>
        /// <returns>The same route builder.</returns>
        public IResolvedRouteTestBuilder AndAlso()
        {
            return this;
        }

        private IAndResolvedRouteTestBuilder ResolveTo<TController>(LambdaExpression actionCall)
            where TController : Controller
        {
            this.actionCallExpression = actionCall;
            this.ValidateRouteInformation<TController>();
            return this;
        }

        private void ValidateRouteInformation<TController>()
            where TController : Controller
        {
            var expectedRouteValues = this.GetExpectedRouteInfo<TController>();
            var actualRouteValues = this.GetActualRouteInfo();

            if (!actualRouteValues.IsResolved)
            {
                this.ThrowNewRouteAssertionException(actual: actualRouteValues.UnresolvedError);
            }

            if (Reflection.AreDifferentTypes(this.expectedRouteInfo.ControllerType, this.actualRouteInfo.ControllerType))
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "instead matched {0}",
                    actualRouteValues.ControllerType));
            }

            if (expectedRouteValues.Action != actualRouteValues.Action)
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "instead matched {0} action",
                    actualRouteValues.Action));
            }

            expectedRouteValues.RouteValues.ForEach(arg =>
            {
                if (!actualRouteValues.RouteValues.ContainsKey(arg.Key))
                {
                    this.ThrowNewRouteAssertionException(actual: string.Format(
                        "the '{0}' parameter could not be found",
                        arg.Key));
                }

                var expectedArgumentInfo = arg.Value;
                var actualArgumentInfo = actualRouteValues.RouteValues[arg.Key];
                if (Reflection.AreNotDeeplyEqual(expectedArgumentInfo.Value, actualArgumentInfo.Value))
                {
                    this.ThrowNewRouteAssertionException(actual: string.Format(
                        "the '{0}' parameter was different",
                        arg.Key));
                }
            });
        }

        private ExpressionParsedRouteContext GetExpectedRouteInfo<TController>()
            where TController : Controller
        {
            return this.expectedRouteInfo ??
                   (this.expectedRouteInfo = RouteExpressionParser.Parse<TController>(this.actionCallExpression));
        }

        private ResolvedRouteContext GetActualRouteInfo()
        {
            return this.actualRouteInfo ??
                   (this.actualRouteInfo = InternalRouteResolver.Resolve(this.Router, this.routeContext));
        }

        private void ThrowNewRouteAssertionException(string expected = null, string actual = null)
        {
            if (string.IsNullOrWhiteSpace(expected))
            {
                expected = string.Format(
                    "match {0} action in {1}",
                    this.expectedRouteInfo.Action,
                    this.expectedRouteInfo.ControllerType);
            }

            throw new RouteAssertionException(string.Format(
                    "Expected route '{0}' to {1} but {2}.",
                    this.routeContext.HttpContext.Request.Path,
                    expected,
                    actual));
        }
    }
}
