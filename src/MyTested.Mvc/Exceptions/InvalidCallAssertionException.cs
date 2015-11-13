namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid test call.
    /// </summary>
    public class InvalidCallAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidCallAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public InvalidCallAssertionException(string message)
            : base(message)
        {
        }
    }
}
