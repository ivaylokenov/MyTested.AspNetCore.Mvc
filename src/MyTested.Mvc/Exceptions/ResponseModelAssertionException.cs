namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid action return type when expecting response model.
    /// </summary>
    public class ResponseModelAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ResponseModelAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ResponseModelAssertionException(string message)
            : base(message)
        {
        }
    }
}
