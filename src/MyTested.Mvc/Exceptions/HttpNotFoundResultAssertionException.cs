namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid HTTP not found result.
    /// </summary>
    public class HttpNotFoundResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ActionCallAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpNotFoundResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
