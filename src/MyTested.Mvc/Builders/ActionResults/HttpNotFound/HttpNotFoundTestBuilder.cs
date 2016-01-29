namespace MyTested.Mvc.Builders.ActionResults.HttpNotFound
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Contracts.ActionResults.HttpNotFound;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    /// <typeparam name="THttpNotFoundResult">Type of not found result - HttpNotFoundResult or HttpNotFoundObjectResult.</typeparam>
    public class HttpNotFoundTestBuilder<THttpNotFoundResult>
        : BaseTestBuilderWithResponseModel<THttpNotFoundResult>, IAndHttpNotFoundTestBuilder
        where THttpNotFoundResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="httpNotFoundResult">Result from the tested action.</param>
        public HttpNotFoundTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            THttpNotFoundResult httpNotFoundResult)
            : base(controller, actionName, caughtException, httpNotFoundResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the HTTP not found result.
        /// </summary>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder WithNoResponseModel()
        {
            var actualResult = this.ActionResult as HttpNotFoundResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model, but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder WithStatusCode(int statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the content type provided as string.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the content type provided as MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as enumerable of strings.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of strings.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as enumerable of MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentTypes">Content types as enumerable of MediaTypeHeaderValue.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the same content types provided as MediaTypeHeaderValue parameters.
        /// </summary>
        /// <param name="contentTypes">Content types as MediaTypeHeaderValue parameters.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the provided output formatter.
        /// </summary>
        /// <param name="outputFormatter">Instance of IOutputFormatter.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains output formatter of the provided type.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of IOutputFormatter.</typeparam>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Enumerable of IOutputFormatter.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP not found result contains the provided output formatters.
        /// </summary>
        /// <param name="outputFormatters">Output formatter parameters.</param>
        /// <returns>The same HTTP not found test builder.</returns>
        public IAndHttpNotFoundTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining HTTP not found result tests.
        /// </summary>
        /// <returns>HTTP not found result test builder.</returns>
        public IAndHttpNotFoundTestBuilder AndAlso() => this;

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <returns>Action result to be tested.</returns>
        public new ActionResult AndProvideTheActionResult() => this.ActionResult;

        /// <summary>
        /// Throws new HTTP not found result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed..</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewHttpNotFoundResultAssertionException(propertyName, expectedValue, actualValue);

        private void ThrowNewHttpNotFoundResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpNotFoundResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP not found result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
