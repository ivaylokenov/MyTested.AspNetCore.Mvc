namespace MyTested.Mvc.Builders.ActionResults.Redirect
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Base;
    using Contracts.ActionResults.Redirect;
    using Contracts.Uris;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    /// <typeparam name="TRedirectResult">Type of redirect result - RedirectResult, RedirectToActionResult or RedirectToRouteResult.</typeparam>
    public class RedirectTestBuilder<TRedirectResult>
        : BaseTestBuilderWithActionResult<TRedirectResult>, IAndRedirectTestBuilder
        where TRedirectResult : ActionResult
    {
        private const string Location = "location";
        private const string RouteName = "route name";

        private LambdaExpression redirectToExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectTestBuilder{TRedirectResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="redirectResult">Result from the tested action.</param>
        public RedirectTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Tests whether redirect result is permanent.
        /// </summary>
        /// <returns>The same redirect test builder.</returns>
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

        /// <summary>
        /// Tests whether redirect result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ToUrl(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewRedirectResultAssertionException);
            return this.ToUrl(uri);
        }

        /// <summary>
        /// Tests whether redirect result location passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the location.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ToUrlPassing(Action<string> assertions)
        {
            var redirectResult = this.GetRedirectResult<RedirectResult>(Location);
            assertions(redirectResult.Url);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result location passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same redirect test builder.</returns>
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
        
        /// <summary>
        /// Tests whether redirect result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ToUrl(Uri location)
        {
            var redirrectResult = this.GetRedirectResult<RedirectResult>(Location);
            LocationValidator.ValidateUri(
                this.ActionResult,
                location.OriginalString,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.GetRedirectResult<RedirectResult>(Location),
                uriTestBuilder,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }
        
        /// <summary>
        /// Tests whether redirect at action result has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ToAction(string actionName)
        {
            var redirectAtActionResult = this.GetRedirectResult<RedirectToActionResult>("action name");
            RouteActionResultValidator.ValidateActionName(
                redirectAtActionResult,
                actionName,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect at action result has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ToController(string controllerName)
        {
            var redirectAtActionResult = this.GetRedirectResult<RedirectToActionResult>("controller name");
            RouteActionResultValidator.ValidateControllerName(
                redirectAtActionResult,
                controllerName,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect at route result has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder WithRouteName(string routeName)
        {
            var redirectAtRouteResult = this.GetRedirectResult<RedirectToRouteResult>("route name");
            RouteActionResultValidator.ValidateRouteName(
                redirectAtRouteResult,
                routeName,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ContainingRouteKey(string key)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                key,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ContainingRouteValue(string key, object value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                key,
                value,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }
        
        public IAndRedirectTestBuilder ContainingRouteValue<TRouteValue>(TRouteValue value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                value,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        public IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>()
        {
            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                this.ActionResult,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        public IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>(string key)
        {
            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                this.ActionResult,
                key,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder ContainingRouteValues(object routeValues)
            => this.ContainingRouteValues(new RouteValueDictionary(routeValues));

        /// <summary>
        /// Tests whether redirect result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same redirect test builder.</returns>
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

        /// <summary>
        /// Tests whether redirect result has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper)
        {
            RouteActionResultValidator.ValidateUrlHelper(
                this.ActionResult,
                urlHelper,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type IUrlHelper.</typeparam>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper
        {
            RouteActionResultValidator.ValidateUrlHelperOfType<TUrlHelper>(
                this.ActionResult,
                this.ThrowNewRedirectResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same redirect test builder.</returns>
        public IAndRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : class
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        public IAndRedirectTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class
        {
            return this.ProcessRouteLambdaExpression<TController>(actionCall);
        }

        /// <summary>
        /// AndAlso method for better readability when chaining redirect result tests.
        /// </summary>
        /// <returns>Redirect result test builder.</returns>
        public IRedirectTestBuilder AndAlso() => this;

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <returns>Action result to be tested.</returns>
        public new ActionResult AndProvideTheActionResult() => this.ActionResult;

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
