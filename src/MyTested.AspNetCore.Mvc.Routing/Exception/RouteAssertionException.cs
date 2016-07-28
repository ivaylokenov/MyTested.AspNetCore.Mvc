namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid route.
    /// </summary>
    public class RouteAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RouteAssertionException(string message)
            : base(message)
        {
        }
    }
}
