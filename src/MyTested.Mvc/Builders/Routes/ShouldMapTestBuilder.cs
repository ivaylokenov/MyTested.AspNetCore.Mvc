namespace MyTested.Mvc.Builders.Routes
{
    using System;
    using System.Linq.Expressions;
    using Contracts.Routes;
    using Utilities;
    using Microsoft.AspNetCore.Routing;
    using Internal.Routes;
    using Exceptions;
    using Utilities.Extensions;
    using System.Linq;
    using System.Collections.Generic;
    using Internal.TestContexts;
    using System.Threading.Tasks;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public class ShouldMapTestBuilder : BaseRouteTestBuilder, IShouldMapTestBuilder, IAndResolvedRouteTestBuilder
    {
        private const string ExpectedModelStateErrorMessage = "have valid model state with no errors";
        
        private LambdaExpression actionCallExpression;
        private ResolvedRouteContext actualRouteInfo;
        private ExpressionParsedRouteContext expectedRouteInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldMapTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use for the route test.</param>
        /// <param name="location">URI location represented by string.</param>
        public ShouldMapTestBuilder(RouteTestContext testContext)
            : base(testContext)
        {
        }
        
        private RouteContext RouteContext => this.TestContext.RouteContext;

        public IAndResolvedRouteTestBuilder ToAction(string actionName)
        {
            var actualInfo = this.GetActualRouteInfo();
            var actualActionName = actualInfo.Action;

            if (actionName != actualActionName)
            {
                this.ThrowNewRouteAssertionException(
                    $"match {actionName} action",
                    $"in fact mathed {actualActionName}");
            }

            return this;
        }

        public IAndResolvedRouteTestBuilder ToController(string controllerName)
        {
            var actualInfo = this.GetActualRouteInfo();
            var actualControllerName = actualInfo.ControllerName;

            if (controllerName != actualControllerName)
            {
                this.ThrowNewRouteAssertionException(
                    $"match {controllerName} controller",
                    $"in fact mathed {actualControllerName}");
            }

            return this;
        }

        public IAndResolvedRouteTestBuilder ToRouteValue(string key)
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.ActionArguments.ContainsKey(key) && !actualInfo.RouteData.Values.ContainsKey(key))
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "the '{0}' route value could not be found",
                    key));
            }

            return this;
        }

        public IAndResolvedRouteTestBuilder ToRouteValue(string key, object value)
        {
            this.ToRouteValue(key);

            var actualInfo = this.GetActualRouteInfo();

            object routeValue;
            if (actualInfo.ActionArguments.ContainsKey(key))
            {
                routeValue = actualInfo.ActionArguments[key].Value;
            }
            else
            {
                routeValue = actualInfo.RouteData.Values[key];
            }

            var invalid = false;
            if (value is string || routeValue is string)
            {
                invalid = value.ToString() != routeValue.ToString();
            }
            else
            {
                invalid = Reflection.AreNotDeeplyEqual(value, routeValue);
            }

            if (invalid)
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "the '{0}' route value was different",
                    key));
            }

            return this;
        }

        public IAndResolvedRouteTestBuilder ToRouteValues(object routeValues)
            => this.ToRouteValues(new RouteValueDictionary(routeValues));

        public IAndResolvedRouteTestBuilder ToRouteValues(IDictionary<string, object> routeValues)
        {
            if (this.actionCallExpression == null)
            {
                var actualInfo = this.GetActualRouteInfo();

                var expectedItems = routeValues.Count;
                var actualItems = actualInfo.RouteData.Values.Count;

                if (expectedItems != actualItems)
                {
                    this.ThrowNewRouteAssertionException(
                        $"have {expectedItems} {(expectedItems != 1 ? "route values" : "route value")}",
                        $"in fact found {actualItems}");
                }
            }

            routeValues.ForEach(rv => this.ToRouteValue(rv.Key, rv.Value));

            return this;
        }

        public IAndResolvedRouteTestBuilder ToDataToken(string key)
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.RouteData.DataTokens.ContainsKey(key))
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "the '{0}' data token could not be found",
                    key));
            }

            return this;
        }

        public IAndResolvedRouteTestBuilder ToDataToken(string key, object value)
        {
            this.ToDataToken(key);

            var actualInfo = this.GetActualRouteInfo();
            var routeValue = actualInfo.RouteData.DataTokens[key];

            if (Reflection.AreNotDeeplyEqual(value, routeValue))
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "the '{0}' data token was different",
                    key));
            }

            return this;
        }

        public IAndResolvedRouteTestBuilder ToDataTokens(object dataTokens)
            => this.ToDataTokens(new RouteValueDictionary(dataTokens));

        public IAndResolvedRouteTestBuilder ToDataTokens(IDictionary<string, object> dataTokens)
        {
            var actualInfo = this.GetActualRouteInfo();

            var expectedItems = dataTokens.Count;
            var actualItems = actualInfo.RouteData.DataTokens.Count;

            if (expectedItems != actualItems)
            {
                this.ThrowNewRouteAssertionException(
                    $"have {expectedItems} {(expectedItems != 1 ? "data tokens" : "data token")}",
                    $"in fact found {actualItems}");
            }

            dataTokens.ForEach(rv => this.ToDataToken(rv.Key, rv.Value));

            return this;
        }

        public IAndResolvedRouteTestBuilder To<TController>()
            where TController : class
        {
            var actualInfo = this.GetActualRouteInfo();
            var expectedControllerType = typeof(TController);
            var actualControllerType = actualInfo.ControllerType;

            if (Reflection.AreDifferentTypes(expectedControllerType, actualControllerType))
            {
                this.ThrowNewRouteAssertionException(
                    $"match {expectedControllerType.GetName()}",
                    $"in fact mathed {actualControllerType}");
            }

            return this;
        }

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : class
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        public IAndResolvedRouteTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
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
                    "in fact it was resolved successfully");
            }
        }

        /// <summary>
        /// Tests whether the resolved route will have valid model state.
        /// </summary>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToValidModelState()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsResolved)
            {
                this.ThrowNewRouteAssertionException(
                    ExpectedModelStateErrorMessage,
                    actualInfo.UnresolvedError);
            }

            if (!actualInfo.ModelState.IsValid)
            {
                this.ThrowNewRouteAssertionException(
                    ExpectedModelStateErrorMessage,
                    "it had some");
            }

            return this;
        }

        /// <summary>
        /// Tests whether the resolved route will have invalid model state.
        /// </summary>
        /// <param name="withNumberOfErrors">Expected number of errors. If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToInvalidModelState(int? withNumberOfErrors = null)
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsResolved)
            {
                this.ThrowNewRouteAssertionException(
                    "have invalid model state",
                    actualInfo.UnresolvedError);
            }

            var actualModelStateErrors = actualInfo.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                this.ThrowNewRouteAssertionException(
                    $"have invalid model state{(withNumberOfErrors == null ? string.Empty : $" with {withNumberOfErrors} errors")}",
                    withNumberOfErrors == null ? "was in fact valid" : $"in fact contained {actualModelStateErrors}");
            }

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

        private IAndResolvedRouteTestBuilder ProcessRouteLambdaExpression<TController>(LambdaExpression actionCall)
        {
            this.actionCallExpression = actionCall;
            this.ValidateRouteInformation<TController>();
            return this;
        }

        private void ValidateRouteInformation<TController>()
        {
            var expectedRouteValues = this.GetExpectedRouteInfo();
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

            expectedRouteValues.ActionArguments.ForEach(arg => this.ToRouteValue(arg.Key, arg.Value.Value));
        }

        private ExpressionParsedRouteContext GetExpectedRouteInfo()
        {
            return this.expectedRouteInfo ??
                   (this.expectedRouteInfo = RouteExpressionParser.Parse(this.actionCallExpression));
        }

        private ResolvedRouteContext GetActualRouteInfo()
        {
            return this.actualRouteInfo ??
                   (this.actualRouteInfo = InternalRouteResolver.Resolve(this.Services, this.Router, this.RouteContext));
        }

        private void ThrowNewRouteAssertionException(string expected = null, string actual = null)
        {
            if (string.IsNullOrWhiteSpace(expected))
            {
                expected = string.Format(
                    "match {0} action in {1}",
                    this.expectedRouteInfo.Action,
                    this.expectedRouteInfo.ControllerType.ToFriendlyTypeName());
            }

            throw new RouteAssertionException(string.Format(
                "{0} to {1} but {2}.",
                this.TestContext.ExceptionMessagePrefix,
                expected,
                actual));
        }
    }
}
