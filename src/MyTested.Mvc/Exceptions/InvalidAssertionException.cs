namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid assertion.
    /// </summary>
    public class InvalidAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public InvalidAssertionException(string message)
            : base(message)
        {
        }
    }
}
