namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid forbid result.
    /// </summary>
    public class ForbidResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ForbidResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ForbidResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
