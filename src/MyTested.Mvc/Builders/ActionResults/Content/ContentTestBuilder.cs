namespace MyTested.Mvc.Builders.ActionResults.Content
{
    using System;
    using System.Net;
    using System.Text;
    using Base;
    using Contracts.ActionResults.Content;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;

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
        public ContentTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            ContentResult contentResult)
            : base(controller, actionName, caughtException, contentResult)
        {
        }
        
        /// <summary>
        /// Tests whether content result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithStatusCode(int statusCode)
        {
            return this.WithStatusCode((HttpStatusCode)statusCode);
        }

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
            return this.WithContentType(new MediaTypeHeaderValue(contentType));
        }

        /// <summary>
        /// Tests whether content result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithContentType(MediaTypeHeaderValue contentType)
        {
            ContentTypeValidator.ValidateContentType(
                contentType,
                this.ActionResult.ContentType,
                this.ThrowNewContentResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether content result has the default UTF8 encoding.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithDefaultEncoding()
        {
            return this.WithEncoding(new UTF8Encoding(false, true));
        }

        /// <summary>
        /// Tests whether content result has the same encoding as the provided Encoding.
        /// </summary>
        /// <param name="encoding">Expected encoding.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithEncoding(Encoding encoding)
        {
            var actualEncoding = this.ActionResult.ContentType.Encoding;
            if (!encoding.Equals(actualEncoding))
            {
                this.ThrowNewContentResultAssertionException(
                    "encoding",
                    this.GetEncodingName(encoding),
                    this.GetEncodingName(actualEncoding));
            }

            return this;
        }
        
        /// <summary>
        /// AndAlso method for better readability when chaining content tests.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        public IContentTestBuilder AndAlso()
        {
            return this;
        }
        
        private string GetEncodingName(Encoding encoding)
        {
            var fullEncodingName = encoding.ToString();
            var lastIndexOfDot = fullEncodingName.LastIndexOf(".", StringComparison.Ordinal);
            return fullEncodingName.Substring(lastIndexOfDot + 1);
        }

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
