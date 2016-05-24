namespace MyTested.Mvc.Builders.ActionResults.StatusCode
{
    using System.Collections.Generic;
    using Base;
    using Contracts.ActionResults.StatusCode;
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
    /// Used for testing status code result.
    /// </summary>
    /// <typeparam name="TStatusCodeResult">Type of status code result - <see cref="StatusCodeResult"/> or <see cref="ObjectResult"/>.</typeparam>
    public class StatusCodeTestBuilder<TStatusCodeResult>
        : BaseTestBuilderWithResponseModel<TStatusCodeResult>, IAndStatusCodeTestBuilder
        where TStatusCodeResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCodeTestBuilder{TStatusCodeResult}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public StatusCodeTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <inheritdoc />
        public IAndStatusCodeTestBuilder WithNoResponseModel()
        {
            this.WithNoResponseModel<StatusCodeResult>();
            return this;
        }
        
        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingContentType(string contentType)
        {
            this.ValidateObjectResult();
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateObjectResult();
            this.ValidateContainingOfContentType(contentType);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
        {
            this.ValidateObjectResult();
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingContentTypes(params string[] contentTypes)
        {
            this.ValidateObjectResult();
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateObjectResult();
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
        {
            this.ValidateObjectResult();
            this.ValidateContentTypes(contentTypes);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateObjectResult();
            this.ValidateContainingOfOutputFormatter(outputFormatter);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateObjectResult();
            this.ValidateContainingOutputFormatterOfType<TOutputFormatter>();
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateObjectResult();
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IAndStatusCodeTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
        {
            this.ValidateObjectResult();
            this.ValidateOutputFormatters(outputFormatters);
            return this;
        }

        /// <inheritdoc />
        public IStatusCodeTestBuilder AndAlso() => this;

        IShouldPassForTestBuilderWithActionResult<ActionResult> IBaseTestBuilderWithActionResult<ActionResult>.ShouldPassFor()
            => new ShouldPassForTestBuilderWithActionResult<ActionResult>(this.TestContext);

        /// <summary>
        /// Throws new status code result assertion exception for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewStatusCodeResultAssertionException(propertyName, expectedValue, actualValue);
        
        private void ValidateObjectResult()
        {
            var objectResult = this.ActionResult as ObjectResult;
            if (objectResult == null)
            {
                this.ThrowNewStatusCodeResultAssertionException("to inherit", nameof(ObjectResult), "in fact it did not");
            }
        }

        private void ThrowNewStatusCodeResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new StatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected status code result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
