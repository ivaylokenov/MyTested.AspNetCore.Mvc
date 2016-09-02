namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using Internal;
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ViewComponents.ContentViewComponentResult"/>.
    /// </summary>
    public class ContentViewComponentResultAssertionException : Exception
    {
        public static ContentViewComponentResultAssertionException ForEquality(string messagePrefix, string content, string actualContent)
            => new ContentViewComponentResultAssertionException(string.Format(
                ExceptionMessageFormats.ContentResult,
                messagePrefix,
                content,
                actualContent));

        public static ContentViewComponentResultAssertionException ForPredicate(string messagePrefix, string content)
            => new ContentViewComponentResultAssertionException(string.Format(
                ExceptionMessageFormats.ContentResultPredicate,
                messagePrefix,
                content));

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewComponentResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ContentViewComponentResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
