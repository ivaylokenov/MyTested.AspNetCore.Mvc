namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid assertion.
    /// </summary>
    public class InvalidAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidAssertionException(string message)
            : base(message)
        {
        }
    }
}
