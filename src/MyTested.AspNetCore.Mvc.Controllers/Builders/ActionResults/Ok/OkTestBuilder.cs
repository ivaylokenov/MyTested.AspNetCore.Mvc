namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Ok
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Base;
    using Contracts.ActionResults.Ok;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing OK result.
    /// </summary>
    /// <typeparam name="TOkResult">Type of OK result - <see cref="OkResult"/> or <see cref="OkObjectResult"/>.</typeparam>
    public class OkTestBuilder<TOkResult>
        : BaseTestBuilderWithResponseModel<TOkResult>, IAndOkTestBuilder
        where TOkResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkTestBuilder{TOkResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public OkTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IAndOkTestBuilder WithStatusCode(int statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            this.ValidateStatusCode(statusCode);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndOkTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IOkTestBuilder AndAlso() => this;

        public override void ValidateNoModel() => this.WithNoModel<OkResult>();

        /// <summary>
        /// Throws new OK result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewOkResultAssertionException(propertyName, expectedValue, actualValue);
        
        private void ThrowNewOkResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new OkResultAssertionException(string.Format(
                "{0} OK result {1} {2}, but {3}.",
                this.TestContext.ExceptionMessagePrefix,
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
