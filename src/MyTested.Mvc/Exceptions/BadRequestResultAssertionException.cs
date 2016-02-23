namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid bad request result.
    /// </summary>
    public class BadRequestResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BadRequestResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public BadRequestResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
