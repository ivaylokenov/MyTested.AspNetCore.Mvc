namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.OkResult"/> or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/>.
    /// </summary>
    public class OkResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OkResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
