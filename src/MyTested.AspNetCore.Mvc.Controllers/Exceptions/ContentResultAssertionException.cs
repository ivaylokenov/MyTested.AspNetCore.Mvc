namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using Internal;
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
    /// </summary>
    public class ContentResultAssertionException : Exception
    {
        public static ContentResultAssertionException ForEquality(string messagePrefix, string content, string actualContent)
            => new ContentResultAssertionException(string.Format(
                ExceptionMessageFormats.ContentResult,
                messagePrefix,
                content,
                actualContent));

        public static ContentResultAssertionException ForPredicate(string messagePrefix, string content)
            => new ContentResultAssertionException(string.Format(
                ExceptionMessageFormats.ContentResultPredicate,
                messagePrefix,
                content));

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ContentResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
