namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>.
    /// </summary>
    public class ForbidResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbidResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ForbidResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
