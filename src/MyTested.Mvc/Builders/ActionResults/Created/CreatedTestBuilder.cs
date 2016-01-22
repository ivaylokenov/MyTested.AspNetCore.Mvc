namespace MyTested.Mvc.Builders.ActionResults.Created
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Base;
    using Contracts.ActionResults.Created;
    using Contracts.Uris;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Routing;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    /// <typeparam name="TCreatedResult">Type of created result - CreatedAtActionResult or CreatedAtRouteResult.</typeparam>
    public class CreatedTestBuilder<TCreatedResult>
        : BaseTestBuilderWithResponseModel<TCreatedResult>, IAndCreatedTestBuilder
    {
        private const string Location = "location";
        private const string RouteName = "route name";
        private const string RouteValues = "route values";
        private const string UrlHelper = "URL helper";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedTestBuilder{TCreatedResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="createdResult">Result from the tested action.</param>
        public CreatedTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TCreatedResult createdResult)
            : base(controller, actionName, caughtException, createdResult)
        {
        }

        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewCreatedResultAssertionException);
            return this.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(Uri location)
        {
            var createdResult = this.GetCreatedResult<CreatedResult>(Location);
            LocationValidator.ValidateUri(
                createdResult,
                location.OriginalString,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            var createdResult = this.GetCreatedResult<CreatedResult>(Location);
            LocationValidator.ValidateLocation(
                createdResult,
                uriTestBuilder,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created at action result has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtAction(string actionName)
        {
            var createdAtActionResult = this.GetCreatedResult<CreatedAtActionResult>("action name");
            RouteActionResultValidator.ValidateActionName(
                createdAtActionResult,
                actionName,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created at action result has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder AtController(string controllerName)
        {
            var createdAtActionResult = this.GetCreatedResult<CreatedAtActionResult>("controller name");
            RouteActionResultValidator.ValidateControllerName(
                createdAtActionResult,
                controllerName,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created at route result has specific route name.
        /// </summary>
        /// <param name="routeName">Expected route name.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithRouteName(string routeName)
        {
            var createdAtRouteResult = this.GetCreatedResult<CreatedAtRouteResult>("route name");
            RouteActionResultValidator.ValidateRouteName(
                createdAtRouteResult,
                routeName,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result contains specific route key.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithRouteValue(string key)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                key,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result contains specific route key and value.
        /// </summary>
        /// <param name="key">Expected route key.</param>
        /// <param name="value">Expected route value.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithRouteValue(string key, object value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.ActionResult,
                key,
                value,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithRouteValues(object routeValues)
        {
            return this.WithRouteValues(new RouteValueDictionary(routeValues));
        }

        /// <summary>
        /// Tests whether created result contains the provided route values.
        /// </summary>
        /// <param name="routeValues">Expected route value dictionary.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithRouteValues(IDictionary<string, object> routeValues)
        {
            RouteActionResultValidator.ValidateRouteValues(
                this.ActionResult,
                routeValues,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result has the same URL helper as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type IUrlHelper.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithUrlHelper(IUrlHelper urlHelper)
        {
            RouteActionResultValidator.ValidateUrlHelper(
                this.ActionResult,
                urlHelper,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result has the same URL helper type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type IUrlHelper.</typeparam>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper
        {
            RouteActionResultValidator.ValidateUrlHelperOfType<TUrlHelper>(
                this.ActionResult,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder At<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller
        {
            return this.AtRoute<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        public IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller
        {
            return this.AtRoute<TController>(actionCall);
        }

        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>Created test builder.</returns>
        public ICreatedTestBuilder AndAlso()
        {
            return this;
        }

        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewCreatedResultAssertionException(propertyName, expectedValue, actualValue);

        private TExpectedCreatedResult GetCreatedResult<TExpectedCreatedResult>(string containment)
            where TExpectedCreatedResult : class
        {
            var actualRedirectResult = this.ActionResult as TExpectedCreatedResult;
            if (actualRedirectResult == null)
            {
                this.ThrowNewCreatedResultAssertionException(
                    "to contain",
                    containment,
                    "it could not be found");
            }

            return actualRedirectResult;
        }

        private IAndCreatedTestBuilder AtRoute<TController>(LambdaExpression actionCall)
            where TController : Controller
        {
            return this;
        }

        private void ThrowNewCreatedResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new CreatedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected created result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
