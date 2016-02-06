namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid authentication properties call.
    /// </summary>
    public class RouteAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RouteAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public RouteAssertionException(string message)
            : base(message)
        {
        }
    }
}
