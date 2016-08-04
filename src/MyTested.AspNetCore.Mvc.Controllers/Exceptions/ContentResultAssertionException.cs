namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ContentResult"/>.
    /// </summary>
    public class ContentResultAssertionException : Exception
    {
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
