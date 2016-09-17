namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>.
    /// </summary>
    public class StatusCodeResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCodeResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public StatusCodeResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
