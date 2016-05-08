namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid expected exceptions.
    /// </summary>
    public class InvalidExceptionAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidExceptionAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidExceptionAssertionException(string message)
            : base(message)
        {
        }
    }
}
