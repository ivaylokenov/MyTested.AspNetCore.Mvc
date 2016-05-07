namespace MyTested.Mvc.Builders.ActionResults.HttpNotFound
{
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Contracts.ActionResults.NotFound;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using ShouldPassFor;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    /// <typeparam name="THttpNotFoundResult">Type of not found result - <see cref="NotFoundResult"/> or <see cref="NotFoundObjectResult"/>.</typeparam>
    public class NotFoundTestBuilder<THttpNotFoundResult>
        : BaseTestBuilderWithResponseModel<THttpNotFoundResult>, IAndNotFoundTestBuilder
        where THttpNotFoundResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundTestBuilder{TActionResult}"/> class.
        /// </summary>
        /// <param name="testContext">Controller test context containing data about the currently executed assertion chain.</param>
        public NotFoundTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder WithNoResponseModel()
        {
            var actualResult = this.ActionResult as NotFoundResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model, but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder WithStatusCode(int statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndNotFoundTestBuilder AndAlso() => this;

        IShouldPassForTestBuilderWithActionResult<ActionResult> IBaseTestBuilderWithActionResult<ActionResult>.ShouldPassFor()
            => new ShouldPassForTestBuilderWithActionResult<ActionResult>(this.TestContext);

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
            throw new NotFoundResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP not found result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
