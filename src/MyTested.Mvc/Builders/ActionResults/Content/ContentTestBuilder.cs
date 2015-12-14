namespace MyTested.Mvc.Builders.ActionResults.Content
{
    using System;
    using System.Net;
    using Common.Extensions;
    using Exceptions;
    using Contracts.ActionResults.Content;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using System.Text;
    using Base;

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
        /// <param name="actionResult">Result from the tested action.</param>
        public ContentTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            ContentResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether content result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same content test builder.</returns>
        public IAndContentTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            var actualStatusCode = (HttpStatusCode?)this.ActionResult.StatusCode;
            if (actualStatusCode != statusCode)
            {
                var actualStatusCodeAsInt = (int?)actualStatusCode;

                throw new ContentResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have {2} ({3}) status code, but received {4} ({5}).",
                    this.ActionName,
                    this.Controller.GetName(),
                    (int)statusCode,
                    statusCode,
                    actualStatusCode != null ? actualStatusCodeAsInt.ToString() : "no status code",
                    actualStatusCode != null ? actualStatusCode.ToString() : "null"));
            }

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
            var actualContentType = this.ActionResult.ContentType;
            if ((contentType == null && actualContentType != null)
                || (contentType != null && actualContentType == null)
                || (contentType != null && contentType.MediaType != actualContentType.MediaType))
            {
                this.ThrowNewContentResultAssertionException(
                    "ContentType",
                    string.Format("to be {0}", contentType != null ? contentType.MediaType : "null"),
                    string.Format("instead received {0}", actualContentType != null ? actualContentType.MediaType : "null"));
            }

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
        /// Tests whether content result has the same encoging as the provided Encoding.
        /// </summary>
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
