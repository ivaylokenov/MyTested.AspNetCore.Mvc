namespace MyTested.Mvc.Builders.ActionResults.Content
{
    using System.Net;
    using Base;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Utilities.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;
    using Internal.TestContexts;

    /// <summary>
    /// Used for testing content result.
    /// </summary>
    public class ContentTestBuilder
        : BaseTestBuilderWithActionResult<ContentResult>, IAndContentTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="contentResult">Result from the tested action.</param>
        public ContentTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }
        
        /// <summary>
        /// Tests whether content result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <summary>
        /// Tests whether content result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                this.ActionResult.StatusCode,
                this.ThrowNewContentResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether content result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithContentType(string contentType)
        {
            ContentTypeValidator.ValidateContentType(
                contentType,
                this.ActionResult.ContentType,
                this.ThrowNewContentResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether content result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithContentType(MediaTypeHeaderValue contentType)
            => this.WithContentType(contentType?.MediaType);
        
        /// <summary>
        /// AndAlso method for better readability when chaining content tests.
        /// </summary>
        /// <returns>The same content test builder.</returns>
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
