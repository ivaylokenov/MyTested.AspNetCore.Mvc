namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid content result.
    /// </summary>
    public class ContentResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContentResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ContentResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
