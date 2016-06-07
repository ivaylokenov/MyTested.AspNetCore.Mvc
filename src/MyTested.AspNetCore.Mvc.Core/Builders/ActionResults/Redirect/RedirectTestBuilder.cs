namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Redirect
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Base;
    using Contracts.ActionResults.Redirect;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Contracts.Uris;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ShouldPassFor;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    /// <typeparam name="TRedirectResult">Type of redirect result - <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>.</typeparam>
    public class RedirectTestBuilder<TRedirectResult>
        : BaseTestBuilderWithActionResult<TRedirectResult>, IAndRedirectTestBuilder
        where TRedirectResult : ActionResult
    {
        private const string Location = "location";
        private const string RouteName = "route name";

        private LambdaExpression redirectToExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectTestBuilder{TRedirectResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public RedirectTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder Permanent()
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var dynamicActionResult = this.GetActionResultAsDynamic();
                if (!dynamicActionResult.Permanent)
                {
                    this.ThrowNewRedirectResultAssertionException(
                        "to",
                        "be permanent",
                        "in fact it was not");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToUrl(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewRedirectResultAssertionException);
            return this.ToUrl(uri);
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToUrlPassing(Action<string> assertions)
        {
            var redirectResult = this.GetRedirectResult<RedirectResult>(Location);
            assertions(redirectResult.Url);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToUrlPassing(Func<string, bool> predicate)
        {
            var redirectResult = this.GetRedirectResult<RedirectResult>(Location);
            var url = redirectResult.Url;
            if (!predicate(url))
            {
                this.ThrowNewRedirectResultAssertionException(
                    $"location ('{url}')",
                    "to pass the given predicate",
                    "it failed");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToUrl(Uri location)
        {
            var redirrectResult = this.GetRedirectResult<RedirectResult>(Location);
            LocationValidator.ValidateUri(
                this.ActionResult,
                location.OriginalString,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.GetRedirectResult<RedirectResult>(Location),
                uriTestBuilder,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToAction(string actionName)
        {
            var redirectAtActionResult = this.GetRedirectResult<RedirectToActionResult>("action name");
            RouteActionResultValidator.ValidateActionName(
                redirectAtActionResult,
                actionName,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ToController(string controllerName)
        {
            var redirectAtActionResult = this.GetRedirectResult<RedirectToActionResult>("controller name");
            RouteActionResultValidator.ValidateControllerName(
                redirectAtActionResult,
                controllerName,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder WithRouteName(string routeName)
        {
            var redirectAtRouteResult = this.GetRedirectResult<RedirectToRouteResult>("route name");
            RouteActionResultValidator.ValidateRouteName(
                redirectAtRouteResult,
                routeName,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteKey(string key)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                key,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteValue(string key, object value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                key,
                value,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteValue<TRouteValue>(TRouteValue value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                value,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>()
        {
            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                this.ActionResult,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>(string key)
        {
            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                this.ActionResult,
                key,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteValues(object routeValues)
            => this.ContainingRouteValues(new RouteValueDictionary(routeValues));

        /// <inheritdoc />
        public IAndRedirectTestBuilder ContainingRouteValues(IDictionary<string, object> routeValues)
        {
            var includeCountCheck = this.redirectToExpression == null;

            RouteActionResultValidator.ValidateRouteValues(
                this.ActionResult,
                routeValues,
                includeCountCheck,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper)
        {
            RouteActionResultValidator.ValidateUrlHelper(
                this.ActionResult,
                urlHelper,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper
        {
            RouteActionResultValidator.ValidateUrlHelperOfType<TUrlHelper>(
                this.ActionResult,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : class
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        /// <inheritdoc />
        public IAndRedirectTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        /// <inheritdoc />
        public IRedirectTestBuilder AndAlso() => this;

        IShouldPassForTestBuilderWithActionResult<ActionResult> IBaseTestBuilderWithActionResult<ActionResult>.ShouldPassFor()
            => new ShouldPassForTestBuilderWithActionResult<ActionResult>(this.TestContext);
        
        private TExpectedRedirectResult GetRedirectResult<TExpectedRedirectResult>(string containment)
            where TExpectedRedirectResult : class
        {
            var actualRedirectResult = this.ActionResult as TExpectedRedirectResult;
            if (actualRedirectResult == null)
            {
                this.ThrowNewRedirectResultAssertionException(
                    "to contain",
                    containment,
                    "it could not be found");
            }

            return actualRedirectResult;
        }

        private IAndRedirectTestBuilder ProcessRouteLambdaExpression<TController>(LambdaExpression actionCall)
        {
            this.redirectToExpression = actionCall;

            RouteActionResultValidator.ValidateExpressionLink(
                this.TestContext,
                LinkGenerationTestContext.FromRedirectResult(this.ActionResult),
                actionCall,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        private void ThrowNewRedirectResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new RedirectResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected redirect result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
