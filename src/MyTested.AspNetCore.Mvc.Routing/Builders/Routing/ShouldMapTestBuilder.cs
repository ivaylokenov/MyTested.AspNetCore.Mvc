namespace MyTested.AspNetCore.Mvc.Builders.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts.Routing;
    using Exceptions;
    using Internal.Routing;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public class ShouldMapTestBuilder : BaseRouteTestBuilder, IShouldMapTestBuilder, IAndResolvedRouteTestBuilder
    {
        public const string ExpectedModelStateErrorMessage = "have valid model state with no errors";

        private LambdaExpression actionCallExpression;
        private ResolvedRouteContext actualRouteInfo;
        private ExpressionParsedRouteContext expectedRouteInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldMapTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="RouteTestContext"/> containing data about the currently executed assertion chain.</param>
        public ShouldMapTestBuilder(RouteTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldMapTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="RouteTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="actionCallExpression">Method call expression indicating the expected resolved action.</param>
        protected ShouldMapTestBuilder(
            RouteTestContext testContext,
            LambdaExpression actionCallExpression)
            : this(testContext)
            => this.actionCallExpression = actionCallExpression;

        public RouteContext RouteContext => this.TestContext.RouteContext;

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToAction(string actionName)
        {
            var actualInfo = this.GetActualRouteInfo();
            var actualActionName = actualInfo.Action;

            if (actionName != actualActionName)
            {
                this.ThrowNewRouteAssertionException(
                    $"match {actionName} action",
                    $"in fact matched {actualActionName}");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToController(string controllerName)
        {
            var actualInfo = this.GetActualRouteInfo();
            var actualControllerName = actualInfo.ControllerName;

            if (controllerName != actualControllerName)
            {
                this.ThrowNewRouteAssertionException(
                    $"match {controllerName} controller",
                    $"in fact matched {actualControllerName}");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToRouteValue(string key)
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.ActionArguments.ContainsKey(key) && !actualInfo.RouteData.Values.ContainsKey(key))
            {
                this.ThrowNewRouteAssertionException(
                    $"contain route value with '{key}' key",
                    $"such was not found");
            }

            return this;
        }

        /// <inheritdoc />
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

            bool invalid;
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
                this.ThrowNewRouteAssertionException(
                    $"contain route value with '{key}' key and the provided value",
                    "the value was different");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToRouteValues(object routeValues)
            => this.ToRouteValues(new RouteValueDictionary(routeValues));

        /// <inheritdoc />
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
                        $"contain {expectedItems} {(expectedItems != 1 ? "route values" : "route value")}",
                        $"in fact found {actualItems}");
                }
            }

            routeValues.ForEach(rv => this.ToRouteValue(rv.Key, rv.Value));

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToDataToken(string key)
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.RouteData.DataTokens.ContainsKey(key))
            {
                this.ThrowNewRouteAssertionException(
                    $"contain data token with '{key}' key",
                    $"such was not found");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToDataToken(string key, object value)
        {
            this.ToDataToken(key);

            var actualInfo = this.GetActualRouteInfo();
            var routeValue = actualInfo.RouteData.DataTokens[key];

            if (Reflection.AreNotDeeplyEqual(value, routeValue))
            {
                this.ThrowNewRouteAssertionException(
                    $"contain data token with '{key}' key and the provided value",
                    $"the value was different");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToDataTokens(object dataTokens)
            => this.ToDataTokens(new RouteValueDictionary(dataTokens));

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder ToDataTokens(IDictionary<string, object> dataTokens)
        {
            var actualInfo = this.GetActualRouteInfo();

            var expectedItems = dataTokens.Count;
            var actualItems = actualInfo.RouteData.DataTokens.Count;

            if (expectedItems != actualItems)
            {
                this.ThrowNewRouteAssertionException(
                    $"contain {expectedItems} {(expectedItems != 1 ? "data tokens" : "data token")}",
                    $"in fact found {actualItems}");
            }

            dataTokens.ForEach(rv => this.ToDataToken(rv.Key, rv.Value));

            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder To(string actionName, string controllerName)
        {
            this.ToAction(actionName);
            this.ToController(controllerName);
            return this;
        }

        /// <inheritdoc />
        public IAndResolvedRouteTestBuilder To<TController>()
            where TController : class
        {
            var actualInfo = this.GetActualRouteInfo();
            var expectedControllerType = typeof(TController);
            var actualControllerType = actualInfo.ControllerType.AsType();

            if (Reflection.AreDifferentTypes(expectedControllerType, actualControllerType))
            {
                this.ThrowNewRouteAssertionException(
                    $"match {expectedControllerType.ToFriendlyTypeName()}",
                    $"in fact matched {actualControllerType.ToFriendlyTypeName()}");
            }

            return this;
        }

        /// <inheritdoc />
        public IControllerRouteTestBuilder<TController> To<TController>(Expression<Action<TController>> actionCall)
            where TController : class 
            => this.ProcessRouteLambdaExpression<TController>(actionCall);

        /// <inheritdoc />
        public IControllerRouteTestBuilder<TController> To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class 
            => this.ProcessRouteLambdaExpression<TController>(actionCall);

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IResolvedRouteTestBuilder AndAlso() => this;

        private IControllerRouteTestBuilder<TController> ProcessRouteLambdaExpression<TController>(LambdaExpression actionCall)
        {
            this.actionCallExpression = actionCall;

            this.ValidateRouteInformation();

            return new ControllerRouteTestBuilder<TController>(this.TestContext, actionCall);
        }

        private void ValidateRouteInformation()
        {
            var expectedRouteValues = this.GetExpectedRouteInfo();
            var actualRouteValues = this.GetActualRouteInfo();

            if (!actualRouteValues.IsResolved)
            {
                this.ThrowNewRouteAssertionException(actual: actualRouteValues.UnresolvedError);
            }

            var expectedControllerType = this.expectedRouteInfo.ControllerType;
            var actualControllerType = this.actualRouteInfo.ControllerType.AsType();

            if (Reflection.AreDifferentTypes(expectedControllerType, actualControllerType))
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "instead matched {0}",
                    actualControllerType.ToFriendlyTypeName()));
            }

            if (expectedRouteValues.Action != actualRouteValues.Action)
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "instead matched {0} action",
                    actualRouteValues.Action));
            }

            expectedRouteValues.ActionArguments.ForEach(arg =>
            {
                var value = arg.Value.Value;
                if (ExpressionParser.IsIgnoredArgument(value))
                {
                    return;
                }

                this.ToRouteValue(arg.Key, value);
            });
        }

        public ExpressionParsedRouteContext GetExpectedRouteInfo()
            => this.expectedRouteInfo ??
                   (this.expectedRouteInfo = RouteExpressionParser.Parse(this.actionCallExpression));

        public ResolvedRouteContext GetActualRouteInfo()
            => this.actualRouteInfo ??
                   (this.actualRouteInfo = MvcRouteResolver.Resolve(
                       this.Services, 
                       this.Router, 
                       this.RouteContext,
                       this.TestContext.FullExecution));

        public void ThrowNewRouteAssertionException(string expected = null, string actual = null)
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
