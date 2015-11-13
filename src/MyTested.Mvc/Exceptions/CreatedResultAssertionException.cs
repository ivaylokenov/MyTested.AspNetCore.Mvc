namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid created result.
    /// </summary>
    public class CreatedResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the CreatedResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public CreatedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
