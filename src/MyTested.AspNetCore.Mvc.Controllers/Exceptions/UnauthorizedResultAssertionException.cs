namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>.
    /// </summary>
    public class UnauthorizedResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnauthorizedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
