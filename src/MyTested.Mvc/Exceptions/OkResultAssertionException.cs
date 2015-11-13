namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid ok result.
    /// </summary>
    public class OkResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the OkResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public OkResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
