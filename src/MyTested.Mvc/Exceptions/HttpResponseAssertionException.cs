namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid HTTP response.
    /// </summary>
    public class HttpResponseAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpResponseAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpResponseAssertionException(string message)
            : base(message)
        {
        }
    }
}
