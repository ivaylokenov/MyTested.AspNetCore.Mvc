namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid HTTP request.
    /// </summary>
    public class InvalidHttpRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidHttpRequestMessageException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public InvalidHttpRequestException(string message)
            : base(message)
        {
        }
    }
}
