namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid test call.
    /// </summary>
    public class InvalidCallAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCallAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCallAssertionException(string message)
            : base(message)
        {
        }
    }
}
