namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Content
{
    using System.Net;
    using Base;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="ContentResult"/>.
    /// </summary>
    public class ContentTestBuilder
        : BaseTestBuilderWithActionResult<ContentResult>, IAndContentTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ContentTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndContentTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <inheritdoc />
        public IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                this.ActionResult.StatusCode,
                this.ThrowNewContentResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndContentTestBuilder WithContentType(string contentType)
        {
            ContentTypeValidator.ValidateContentType(
                contentType,
                this.ActionResult.ContentType,
                this.ThrowNewContentResultAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndContentTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType);

        /// <inheritdoc />
        public IContentTestBuilder AndAlso() => this;
        
        private void ThrowNewContentResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new ContentResultAssertionException(string.Format(
                "When calling {0} action in {1} expected content result {2} {3}, but {4}.",
                this.ActionName,
                this.Controller.GetName(),
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
