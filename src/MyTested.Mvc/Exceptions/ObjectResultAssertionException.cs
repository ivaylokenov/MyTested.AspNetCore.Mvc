namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid challenge result.
    /// </summary>
    public class ObjectResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ObjectResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ObjectResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
