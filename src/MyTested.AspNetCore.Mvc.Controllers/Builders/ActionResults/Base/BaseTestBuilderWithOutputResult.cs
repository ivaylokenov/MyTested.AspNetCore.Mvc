namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Builders.Base;
    using Contracts.ActionResults.Base;
    using Contracts.Base;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;
    
    /// <summary>
    /// Base class for all test builders with output <see cref="ActionResult"/>.
    /// </summary>
    /// <typeparam name="TOutputResult">Output result from invoked action in ASP.NET Core MVC controller.</typeparam>
    /// <typeparam name="TOutputResultTestBuilder">Type of output result test builder to use as a return type for common methods.</typeparam>
    public abstract class BaseTestBuilderWithOutputResult<TOutputResult, TOutputResultTestBuilder>
        : BaseTestBuilderWithResponseModel<TOutputResult>, IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> 
        where TOutputResult : class
        where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BaseTestBuilderWithOutputResult{TOutputResult, TOutputResultTestBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseTestBuilderWithOutputResult(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the output result test builder.
        /// </summary>
        /// <value>Test builder for the output <see cref="ActionResult"/>.</value>
        protected abstract TOutputResultTestBuilder ResultTestBuilder { get; }

        /// <inheritdoc />
        public TOutputResultTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <inheritdoc />
        public TOutputResultTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                this.TestContext.MethodResult,
                statusCode,
                this.ThrowNewFailedValidationException);

            return this.ResultTestBuilder;
        }

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingContentType(string contentType)
            => this.ContainingContentType(new MediaTypeHeaderValue(contentType));

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingContentType(MediaTypeHeaderValue contentType)
        {
            this.ValidateObjectResult();

            ContentTypeValidator.ValidateContainingOfContentType(
                this.TestContext.MethodResult,
                contentType,
                this.ThrowNewFailedValidationException);

            return this.ResultTestBuilder;
        }

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingContentTypes(IEnumerable<string> contentTypes)
            => this.ContainingContentTypes(contentTypes.Select(ct => new MediaTypeHeaderValue(ct)));

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingContentTypes(params string[] contentTypes)
            => this.ContainingContentTypes(contentTypes.AsEnumerable());

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            this.ValidateObjectResult();

            ContentTypeValidator.ValidateContentTypes(
                this.TestContext.MethodResult,
                contentTypes,
                this.ThrowNewFailedValidationException);

            return this.ResultTestBuilder;
        }

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingContentTypes(params MediaTypeHeaderValue[] contentTypes)
            => this.ContainingContentTypes(contentTypes.AsEnumerable());

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingOutputFormatter(IOutputFormatter outputFormatter)
        {
            this.ValidateObjectResult();

            OutputFormatterValidator.ValidateContainingOfOutputFormatter(
                this.GetObjectResult(),
                outputFormatter,
                this.ThrowNewFailedValidationException);

            return this.ResultTestBuilder;
        }

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>()
            where TOutputFormatter : IOutputFormatter
        {
            this.ValidateObjectResult();

            OutputFormatterValidator.ValidateContainingOutputFormatterOfType<TOutputFormatter>(
                this.GetObjectResult(),
                this.ThrowNewFailedValidationException);

            return this.ResultTestBuilder;
        }

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingOutputFormatters(IEnumerable<IOutputFormatter> outputFormatters)
        {
            this.ValidateObjectResult();

            OutputFormatterValidator.ValidateOutputFormatters(
                this.GetObjectResult(),
                outputFormatters,
                this.ThrowNewFailedValidationException);

            return this.ResultTestBuilder;
        }

        /// <inheritdoc />
        public TOutputResultTestBuilder ContainingOutputFormatters(params IOutputFormatter[] outputFormatters)
            => this.ContainingOutputFormatters(outputFormatters.AsEnumerable());
        
        private void ValidateObjectResult()
        {
            if (!(this.TestContext.MethodResult is ObjectResult))
            {
                this.ThrowNewFailedValidationException("to inherit", nameof(ObjectResult), "in fact it did not");
            }
        }
    }
}
