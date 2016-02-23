namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid not found result.
    /// </summary>
    public class NotFoundResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NotFoundResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public NotFoundResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
