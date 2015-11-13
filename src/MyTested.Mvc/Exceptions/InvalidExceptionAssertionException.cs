namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid expected exceptions.
    /// </summary>
    public class InvalidExceptionAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidExceptionAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public InvalidExceptionAssertionException(string message)
            : base(message)
        {
        }
    }
}
