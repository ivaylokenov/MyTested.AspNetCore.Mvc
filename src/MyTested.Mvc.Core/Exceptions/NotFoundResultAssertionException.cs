namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> or <see cref="Microsoft.AspNetCore.Mvc.NotFoundObjectResult"/>.
    /// </summary>
    public class NotFoundResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
