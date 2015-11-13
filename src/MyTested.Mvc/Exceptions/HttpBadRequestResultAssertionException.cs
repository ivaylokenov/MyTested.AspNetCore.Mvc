namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid bad request result.
    /// </summary>
    public class HttpBadRequestResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpBadRequestResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpBadRequestResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
