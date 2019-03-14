namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Created
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Builders.Base;
    using Contracts.ActionResults.Created;
    using Contracts.Uri;
    using Exceptions;
    using Internal;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    /// <typeparam name="TCreatedResult">Type of created result - <see cref="CreatedResult"/>, <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>.</typeparam>
    public class CreatedTestBuilder<TCreatedResult>
        : BaseTestBuilderWithResponseModel<TCreatedResult>, IAndCreatedTestBuilder
        where TCreatedResult : ObjectResult
    {
        private const string Location = "location";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedTestBuilder{TCreatedResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public CreatedTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        public bool IncludeCountCheck { get; set; } = true;

        /// <inheritdoc />
        public IAndCreatedTestBuilder WithStatusCode(int statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewCreatedResultAssertionException);
            return this.AtLocation(uri);
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtLocationPassing(Action<string> assertions)
        {
            var createdResult = this.GetCreatedResult<CreatedResult>(Location);
            assertions(createdResult.Location);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtLocationPassing(Func<string, bool> predicate)
        {
            var createdResult = this.GetCreatedResult<CreatedResult>(Location);
            var location = createdResult.Location;
            if (!predicate(location))
            {
                this.ThrowNewCreatedResultAssertionException(
                    $"location ('{location}')",
                    "to pass the given predicate",
                    "it failed");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtLocation(Uri location)
        {
            var createdResult = this.GetCreatedResult<CreatedResult>(Location);
            LocationValidator.ValidateUri(
                createdResult,
                location.OriginalString,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            var createdResult = this.GetCreatedResult<CreatedResult>(Location);
            LocationValidator.ValidateLocation(
                createdResult,
                uriTestBuilder,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtAction(string actionName)
        {
            var createdAtActionResult = this.GetCreatedResult<CreatedAtActionResult>("action name");
            RouteActionResultValidator.ValidateActionName(
                createdAtActionResult,
                actionName,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder AtController(string controllerName)
        {
            var createdAtActionResult = this.GetCreatedResult<CreatedAtActionResult>("controller name");
            RouteActionResultValidator.ValidateControllerName(
                createdAtActionResult,
                controllerName,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder WithRouteName(string routeName)
        {
            var createdAtRouteResult = this.GetCreatedResult<CreatedAtRouteResult>("route name");
            RouteActionResultValidator.ValidateRouteName(
                createdAtRouteResult,
                routeName,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteKey(string key)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.TestContext.MethodResult,
                key,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteValue<TRouteValue>(TRouteValue value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.TestContext.MethodResult,
                value,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteValueOfType<TRouteValue>()
        {
            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                this.TestContext.MethodResult,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteValueOfType<TRouteValue>(string key)
        {
            RouteActionResultValidator.ValidateRouteValueOfType<TRouteValue>(
                this.TestContext.MethodResult,
                key,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteValue(string key, object value)
        {
            RouteActionResultValidator.ValidateRouteValue(
                this.TestContext.MethodResult,
                key,
                value,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteValues(object routeValues)
            => this.ContainingRouteValues(new RouteValueDictionary(routeValues));

        /// <inheritdoc />
        public IAndCreatedTestBuilder ContainingRouteValues(IDictionary<string, object> routeValues)
        {
            RouteActionResultValidator.ValidateRouteValues(
                this.TestContext.MethodResult,
                routeValues,
                this.IncludeCountCheck,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder WithUrlHelper(IUrlHelper urlHelper)
        {
            RouteActionResultValidator.ValidateUrlHelper(
                this.TestContext.MethodResult,
                urlHelper,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndCreatedTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper
        {
            RouteActionResultValidator.ValidateUrlHelperOfType<TUrlHelper>(
                this.TestContext.MethodResult,
                this.ThrowNewCreatedResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public ICreatedTestBuilder AndAlso() => this;
        
        /// <summary>
        /// Throws new created result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewCreatedResultAssertionException(propertyName, expectedValue, actualValue);

        private TExpectedCreatedResult GetCreatedResult<TExpectedCreatedResult>(string containment)
            where TExpectedCreatedResult : class
        {
            var actualRedirectResult = this.TestContext.MethodResult as TExpectedCreatedResult;
            if (actualRedirectResult == null)
            {
                this.ThrowNewCreatedResultAssertionException(
                    "to contain",
                    containment,
                    "it could not be found");
            }

            return actualRedirectResult;
        }

        public void ThrowNewCreatedResultAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new CreatedResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "created",
                propertyName,
                expectedValue,
                actualValue));
    }
}
