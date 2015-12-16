namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid redirect result validation.
    /// </summary>
    public class RedirectResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RedirectResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public RedirectResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
